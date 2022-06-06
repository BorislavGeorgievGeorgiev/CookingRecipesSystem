﻿namespace CookingRecipesSystem.Domain.Common
{
	public abstract class Entity<TKey> : IEntity<TKey>
	{
		public virtual TKey Id { get; set; }
	}
}
