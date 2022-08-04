using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Infrastructure.Common;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CookingRecipesSystem.Infrastructure.Services
{
	public class JwtService : IJwtService
	{
		private const string ClaimId = nameof(ClaimTypes.NameIdentifier);
		private const string ClaimEmail = nameof(ClaimTypes.Email);
		private const string ClaimName = nameof(ClaimTypes.Name);
		private const string ClaimRole = nameof(ClaimTypes.Role);

		private readonly JwtConfig _jwtConfig;
		private readonly IUserManagerService _userManager;
		private readonly IDateTimeService _dateTimeService;

		public JwtService(IOptions<JwtConfig> jwtConfig,
			IUserManagerService userManager,
			IDateTimeService dateTimeService)
		{
			this._jwtConfig = jwtConfig.Value;
			this._userManager = userManager;
			this._dateTimeService = dateTimeService;
		}

		public async Task<string> GenerateToken(string userId, string userEmail)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(this._jwtConfig.Secret);
			var currentUser = await this._userManager.FindByIdAsync(userId);
			var currentUserRoles = await this._userManager.GetRolesAsync(currentUser.Response);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Issuer = this._jwtConfig.ValidIssuer,
				Audience = this._jwtConfig.ValidAudience,
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimId, userId),
					new Claim(ClaimEmail, userEmail),
					new Claim(ClaimName, currentUser.Response.UserName)
				}),
				NotBefore = this._dateTimeService.Now,
				Expires = this._dateTimeService.Now.AddMinutes(
					double.Parse(this._jwtConfig.ExpirationInMinutes)),
				SigningCredentials = new SigningCredentials(
					new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			foreach (var role in currentUserRoles.Response)
			{
				var claimRole = new Claim(ClaimRole, role);
				tokenDescriptor.Subject.AddClaim(claimRole);
			}

			var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
			var encryptedToken = tokenHandler.WriteToken(token);

			return encryptedToken;
		}
	}
}
