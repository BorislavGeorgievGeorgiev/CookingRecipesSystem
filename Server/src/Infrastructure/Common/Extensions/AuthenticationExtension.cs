using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CookingRecipesSystem.Infrastructure.Common.Extensions
{
	public static class AuthenticationExtension
	{
		public static IServiceCollection AddTokenAuthentication(
			this IServiceCollection services, IConfiguration config)
		{
			const string JwtConfigName = nameof(JwtConfig);
			const string JwtSecretName = nameof(JwtConfig.Secret);
			const string ValidIssuer = nameof(JwtConfig.ValidIssuer);
			const string ValidAudience = nameof(JwtConfig.ValidAudience);

			var configuration = config.GetSection(JwtConfigName);
			var secret = configuration.GetSection(JwtSecretName).Value;
			var validIssuer = configuration.GetSection(ValidIssuer).Value;
			var validAudience = configuration.GetSection(ValidAudience).Value;

			var key = Encoding.ASCII.GetBytes(secret);

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.RequireHttpsMetadata = true;
				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuerSigningKey = true,
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidIssuer = validIssuer,
					ValidAudience = validAudience
				};
			});

			return services;
		}
	}
}
