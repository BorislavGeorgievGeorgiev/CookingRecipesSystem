using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Infrastructure.Common;
using CookingRecipesSystem.Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CookingRecipesSystem.Infrastructure.Services
{
	public class JwtService : IJwtService
	{
		private readonly JwtConfig _jwtConfig;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IDateTimeService _dateTimeService;

		public JwtService(IOptions<JwtConfig> jwtConfig,
			UserManager<ApplicationUser> userManager,
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

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.NameIdentifier, userId),
					new Claim(ClaimTypes.Email, userEmail)
				}),
				Expires = this._dateTimeService
				.Now
				.AddMinutes(double.Parse(this._jwtConfig.ExpirationInMinutes)),
				SigningCredentials = new SigningCredentials(
					new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var currentUser = await this._userManager.FindByIdAsync(userId);
			var currentUserRoles = await this._userManager.GetRolesAsync(currentUser);

			foreach (var role in currentUserRoles)
			{
				tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role));
			}

			var token = tokenHandler.CreateToken(tokenDescriptor);
			var encryptedToken = tokenHandler.WriteToken(token);

			return encryptedToken;
		}
	}
}
