using CookingRecipesSystem.Domain.Common.Constants;
using CookingRecipesSystem.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookingRecipesSystem.Infrastructure.Persistence.Configurations
{
	public class ImageConfiguration :
		AuditableEntityConfiguration<Image>, IEntityTypeConfiguration<Image>
	{
		public void Configure(EntityTypeBuilder<Image> builder)
		{
			builder.HasKey(i => i.Id);

			builder.Property(i => i.Original).IsRequired()
				.HasColumnType(AppConstants.ColumnTypeImage);

			builder.Property(i => i.Thumbnail).IsRequired()
				.HasColumnType(AppConstants.ColumnTypeImage);

			this.SetAuditableEntity(builder);
		}
	}
}
