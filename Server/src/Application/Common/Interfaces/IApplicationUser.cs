using CookingRecipesSystem.Domain.Common;

namespace CookingRecipesSystem.Application.Common.Interfaces
{
	public interface IApplicationUser : IAggregateRoot
	{
		public string Id { get; set; }
	}
}
