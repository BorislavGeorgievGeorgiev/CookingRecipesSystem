using CookingRecipesSystem.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookingRecipesSystem.Infrastructure.Persistence.Configurations
{
	public class TestEntityConfiguration :
		AuditableEntityConfiguration<TestEntity>, IEntityTypeConfiguration<TestEntity>
	{
		public void Configure(EntityTypeBuilder<TestEntity> builder)
		{
			builder.HasKey(e => e.Id);

			builder.Property(e => e.Text).HasMaxLength(1500);

			this.SetAuditableEntity(builder);
		}
	}
}
