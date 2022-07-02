﻿using CommunityToolkit.Diagnostics;

using CookingRecipesSystem.Domain.Common;

namespace CookingRecipesSystem.Domain.Entities
{
	public class Recipe : AuditableEntity<int>
	{
		private string? _title;
		private string? _description;

		public Recipe(string title, string description, string createdBy)
		{
			this.Title = title;
			this.Description = description;
			this.CreatedBy = createdBy;
		}

		public string Title
		{
			get { return this._title!; }
			private set
			{
				Guard.IsNotNullOrWhiteSpace(value, nameof(this.Title));
				this._title = value;
			}
		}

		public string Description
		{
			get { return this._description!; }
			private set
			{
				Guard.IsNotNullOrWhiteSpace(value, nameof(this.Description));
				this._description = value;
			}
		}

		public ICollection<RecipeTask> RecipeContents { get; } = new List<RecipeTask>();

		public ICollection<Ingredient> Ingredients { get; } = new List<Ingredient>();
	}
}
