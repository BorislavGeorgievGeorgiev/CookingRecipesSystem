using CookingRecipesSystem.Domain.Common;
using CookingRecipesSystem.Domain.Common.Constants;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookingRecipesSystem.Infrastructure.Persistence.Configurations
{
	public class AuditableEntityConfiguration<TEntity>
		where TEntity : class, IAuditableEntity
	{
		public void SetAuditableEntity(EntityTypeBuilder<TEntity> builder)
		{
			builder.Property(r => r.CreatedBy).IsRequired()
				.HasMaxLength(EntityConstants.CreatedByMaxLength);

			builder.Property(r => r.ModifiedBy)
				.HasMaxLength(EntityConstants.ModifiedByMaxLength);
		}
	}
}
