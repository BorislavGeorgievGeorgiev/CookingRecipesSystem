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
			_jwtConfig = jwtConfig.Value;
			_userManager = userManager;
			_dateTimeService = dateTimeService;
		}

		public async Task<string> GenerateToken(string userId, string userEmail)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
			var currentUser = await _userManager.FindByIdAsync(userId);
			var currentUserRoles = await _userManager.GetRolesAsync(currentUser.Response);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Issuer = _jwtConfig.ValidIssuer,
				Audience = _jwtConfig.ValidAudience,
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimId, userId),
					new Claim(ClaimEmail, userEmail),
					new Claim(ClaimName, currentUser.Response.UserName)
				}),
				NotBefore = _dateTimeService.Now,
				Expires = _dateTimeService.Now.AddMinutes(
					double.Parse(_jwtConfig.ExpirationInMinutes)),
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
