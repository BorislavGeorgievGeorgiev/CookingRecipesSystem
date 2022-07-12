using CookingRecipesSystem.Domain.Common;
using CookingRecipesSystem.Domain.Exceptions;

namespace CookingRecipesSystem.Domain.UnitTests.Common
{
	public class AuditableEntityTests
	{
		private class TestAuditableEntity : AuditableEntity<int> { }

		[Fact]
		public void CreatedBy_Should_Throw_InvalidEntityException_When_Is_Set_With_Null()
		{
			// Arrange
			var auditableEntity = new TestAuditableEntity().SetId(1);

			// Act & Assert
			Assert.Throws<InvalidEntityException>(() => auditableEntity.CreatedBy = null!);
		}

		[Fact]
		public void CreatedOn_Should_Return_Properly_When_Is_Set()
		{
			// Arrange
			var auditableEntity = new TestAuditableEntity().SetId(1);

			// Act
			var dateNow = DateTime.UtcNow;
			auditableEntity.CreatedOn = dateNow;

			// Assert
			Assert.True(auditableEntity.CreatedOn == dateNow);
		}

		[Fact]
		public void ModifiedBy_Should_Throw_InvalidEntityException_When_Is_Set_With_Null()
		{
			// Arrange
			var auditableEntity = new TestAuditableEntity().SetId(1);

			// Act & Assert
			Assert.Throws<InvalidEntityException>(() => auditableEntity.ModifiedBy = null!);
		}

		[Fact]
		public void ModifiedBy_Should_Return_Null_When_Is_Not_Set()
		{
			// Arrange
			var auditableEntity = new TestAuditableEntity().SetId(1);

			// Act & Assert
			Assert.True(auditableEntity.ModifiedBy == null);
		}

		[Fact]
		public void ModifiedOn_Should_Throw_InvalidEntityException_When_Is_Set_With_Null()
		{
			// Arrange
			var auditableEntity = new TestAuditableEntity().SetId(1);

			// Act & Assert
			Assert.Throws<InvalidEntityException>(() => auditableEntity.ModifiedOn = null);
		}

		[Fact]
		public void ModifiedOn_Should_Return_Null_When_Is_Not_Set()
		{
			// Arrange
			var auditableEntity = new TestAuditableEntity().SetId(1);

			// Act & Assert
			Assert.True(auditableEntity.ModifiedOn == null);
		}
	}
}
