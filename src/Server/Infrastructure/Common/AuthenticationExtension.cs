﻿using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CookingRecipesSystem.Infrastructure.Common
{
	public static class AuthenticationExtension
	{
		public static IServiceCollection AddTokenAuthentication(
			this IServiceCollection services, IConfiguration config)
		{
			const string JwtConfigName = nameof(JwtConfig);
			const string JwtSecretName = nameof(JwtConfig.Secret);
			const string? Localhost = "localhost";

			var secret = config.GetSection(JwtConfigName).GetSection(JwtSecretName).Value;

			var key = Encoding.ASCII.GetBytes(secret);

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(bearer =>
			{
				bearer.RequireHttpsMetadata = true;
				bearer.SaveToken = true;
				bearer.TokenValidationParameters = new TokenValidationParameters
				{
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuerSigningKey = true,
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidIssuer = Localhost,
					ValidAudience = Localhost
				};
			});

			return services;
		}
	}
}
