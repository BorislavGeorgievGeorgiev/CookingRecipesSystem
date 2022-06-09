
using CookingRecipesSystem.Domain.Common;

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
