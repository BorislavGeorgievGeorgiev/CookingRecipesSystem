using CookingRecipesSystem.Application.Common.Interfaces;

namespace CookingRecipesSystem.Application.Common.Services
{
	public class DateTimeService : IDateTimeService
	{
		public DateTime Now
		{
			get
			{
				return DateTime.UtcNow;
			}
		}
	}
}
