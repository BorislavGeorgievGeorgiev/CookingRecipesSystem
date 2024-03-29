﻿namespace CookingRecipesSystem.Domain.Common
{
	public interface IAuditableEntity : IDeletableEntity
	{
		public string CreatedBy { get; set; }

		public DateTime CreatedOn { get; set; }

		public string? ModifiedBy { get; set; }

		public DateTime? ModifiedOn { get; set; }
	}
}
