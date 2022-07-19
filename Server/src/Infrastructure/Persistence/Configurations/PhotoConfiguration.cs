using CookingRecipesSystem.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookingRecipesSystem.Infrastructure.Persistence.Configurations
{
	public class PhotoConfiguration :
		AuditableEntityConfiguration<Photo>, IEntityTypeConfiguration<Photo>
	{
		public void Configure(EntityTypeBuilder<Photo> builder)
		{
			builder.HasKey(i => i.Id);

			builder.Property(i => i.MainPhoto).IsRequired();

			builder.Property(i => i.PhonePhoto).IsRequired();

			builder.Property(i => i.Thumbnail).IsRequired();

			this.SetAuditableEntity(builder);
		}
	}
}
