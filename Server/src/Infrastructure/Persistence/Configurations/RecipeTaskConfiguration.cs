﻿using CookingRecipesSystem.Domain.Common.Constants;
using CookingRecipesSystem.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookingRecipesSystem.Infrastructure.Persistence.Configurations
{
	public class RecipeTaskConfiguration :
		AuditableEntityConfiguration<RecipeTask>, IEntityTypeConfiguration<RecipeTask>
	{
		public void Configure(EntityTypeBuilder<RecipeTask> builder)
		{
			builder.HasKey(rt => rt.Id);

			builder.Property(rt => rt.Position).IsRequired();

			builder.Property(rt => rt.Description).IsRequired()
				.HasMaxLength(EntityConstants.RecipeTaskDescriptionMaxLength);

			builder.HasOne(rt => rt.Photo).WithOne()
				.HasForeignKey<RecipeTask>(rt => rt.PhotoId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Ignore(rt => rt.PhotoId);

			SetAuditableEntity(builder);
		}
	}
}
