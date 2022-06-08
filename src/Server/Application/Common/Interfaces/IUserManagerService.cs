﻿using CookingRecipesSystem.Application.Common.Interfaces.Lifetime;
using CookingRecipesSystem.Application.Common.Models;

namespace CookingRecipesSystem.Application.Common.Interfaces
{
	public interface IUserManagerService : ITransientService
	{
		Task<string?> GetUserName(string userId);

		Task<(ApplicationResult Result, string UserId)> CreateUser(string userName, string password);

		Task<ApplicationResult> DeleteUser(string userId);
	}
}
