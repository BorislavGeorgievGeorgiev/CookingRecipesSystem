using CookingRecipesSystem.Application.Common.Interfaces.Lifetime;

namespace CookingRecipesSystem.Application.Common.Interfaces
{
	public interface IDateTimeService : ITransientService
	{
		DateTime Now { get; }
	}
}
