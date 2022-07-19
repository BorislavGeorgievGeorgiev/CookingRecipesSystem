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

			builder.HasOne(i => i.Photo).WithOne()
				.HasConstraintName(nameof(Ingredient) + nameof(Photo) + "Id")
				.HasForeignKey<Photo>(nameof(Ingredient) + nameof(Photo) + "Id")
				.IsRequired(false);

			this.SetAuditableEntity(builder);
		}
	}
}
