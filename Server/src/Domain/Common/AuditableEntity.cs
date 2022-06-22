using CookingRecipesSystem.Domain.Exceptions;

namespace CookingRecipesSystem.Domain.Common
{
	public abstract class AuditableEntity<TKey> : Entity<TKey>, IAuditableEntity
		where TKey : struct
	{
		private string? _createdBy;
		private string? _modifiedBy;

		public string CreatedBy
		{
			get => this._createdBy!;
			set => this._createdBy = value ??
				throw new InvalidEntityException(ExceptionMessages.CreatorIdNotNull);
		}

		public DateTime CreatedOn { get; set; }

		public string ModifiedBy
		{
			get => this._modifiedBy!;
			set => this._modifiedBy = value ??
				throw new InvalidEntityException(ExceptionMessages.ModifierIdNotNull);
		}

		public DateTime? ModifiedOn { get; set; }
	}
}
