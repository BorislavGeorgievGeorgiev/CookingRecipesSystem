
using CookingRecipesSystem.Domain.Common.Constants;
using CookingRecipesSystem.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookingRecipesSystem.Infrastructure.Persistence.Configurations
{
	public class RecipeConfiguration :
		AuditableEntityConfiguration<Recipe>, IEntityTypeConfiguration<Recipe>
	{
		public void Configure(EntityTypeBuilder<Recipe> builder)
		{
			builder.HasKey(r => r.Id);

			builder.Property(r => r.Title).IsRequired()
				.HasMaxLength(EntityConstants.RecipeTitleMaxLength);

			builder.HasIndex(r => r.Title);

			builder.Property(r => r.Description).IsRequired()
				.HasMaxLength(EntityConstants.RecipeDescriptionMaxLength);

			builder.HasOne(r => r.Image).WithOne()
				.HasForeignKey<Image>(im => im.Id).IsRequired();

			builder.HasMany(r => r.Ingredients).WithMany(i => i.Recipes)
				.UsingEntity(j => j.ToTable(nameof(Recipe) + "_" + nameof(Ingredient)));

			builder.HasMany(r => r.RecipeTasks).WithOne()
				.HasForeignKey(nameof(Recipe) + "Id");

			this.SetAuditableEntity(builder);
		}
	}
}
