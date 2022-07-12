using CookingRecipesSystem.Domain.Common.Constants;
using CookingRecipesSystem.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookingRecipesSystem.Infrastructure.Persistence.Configurations
{
	public class IngredientConfiguration :
		AuditableEntityConfiguration<Ingredient>, IEntityTypeConfiguration<Ingredient>
	{
		public void Configure(EntityTypeBuilder<Ingredient> builder)
		{
			builder.HasKey(i => i.Id);

			builder.Property(i => i.Name).IsRequired()
				.HasMaxLength(EntityConstants.IngredientNameMaxLength);

			builder.HasIndex(i => i.Name);

			builder.Property(i => i.Description).IsRequired()
				.HasMaxLength(EntityConstants.IngredientDescriptionMaxLength);

			builder.Property(i => i.MainPhoto).IsRequired()
				.HasColumnType(AppConstants.ColumnTypeImage);

			builder.Property(i => i.ThumbnailPhoto).IsRequired()
				.HasColumnType(AppConstants.ColumnTypeImage);

			this.SetAuditableEntity(builder);
		}
	}
}
