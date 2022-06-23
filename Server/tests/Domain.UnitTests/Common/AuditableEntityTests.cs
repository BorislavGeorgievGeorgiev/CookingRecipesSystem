using CookingRecipesSystem.Domain.Common;
using CookingRecipesSystem.Domain.Exceptions;

namespace CookingRecipesSystem.Domain.UnitTests.Common
{
	public class AuditableEntityTests
	{
		private class TestAuditableEntity : AuditableEntity<int> { }

		[Fact]
		public void AuditableEntity_Should_Throw_InvalidEntityException_When_CreatedBy_Is_Set_With_Null()
		{
			var auditableEntity = new TestAuditableEntity().SetId(1);

			Assert.Throws<InvalidEntityException>(() => auditableEntity.CreatedBy = null!);
		}

		[Fact]
		public void AuditableEntity_Should_Throw_InvalidEntityException_When_ModifiedBy_Is_Set_With_Null()
		{
			var auditableEntity = new TestAuditableEntity().SetId(1);

			Assert.Throws<InvalidEntityException>(() => auditableEntity.ModifiedBy = null!);
		}
	}
}
