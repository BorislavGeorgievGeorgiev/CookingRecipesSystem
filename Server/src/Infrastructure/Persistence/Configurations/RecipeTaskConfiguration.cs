using CookingRecipesSystem.Domain.Common.Constants;
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

			builder.HasIndex(rt => rt.Position).IsUnique();

			builder.Property(rt => rt.Description).IsRequired()
				.HasMaxLength(EntityConstants.RecipeTaskDescriptionMaxLength);

			builder.HasOne(rt => rt.Photo).WithOne()
				.HasForeignKey<Photo>(nameof(RecipeTask) + nameof(RecipeTask.Id))
				.IsRequired(false);

			SetAuditableEntity(builder);
		}
	}
}
