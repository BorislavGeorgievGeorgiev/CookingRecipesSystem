using CookingRecipesSystem.Domain.Exceptions;

namespace CookingRecipesSystem.Domain.Common
{
	public abstract class AuditableEntity<TKey> : Entity<TKey>, IAuditableEntity
		where TKey : struct
	{
		private string? _createdBy;
		private string? _modifiedBy;
		private DateTime? _modifiedOn;

		public string CreatedBy
		{
			get => this._createdBy ??
				throw new InvalidEntityException(ExceptionMessages.CreatorIdNotNull);
			set => this._createdBy = value ??
				throw new InvalidEntityException(ExceptionMessages.CreatorIdNotNull);
		}

		public DateTime CreatedOn { get; set; }

		public string? ModifiedBy
		{
			get => this._modifiedBy;
			set => this._modifiedBy = value ??
				throw new InvalidEntityException(ExceptionMessages.ModifierIdNotNull);
		}

		public DateTime? ModifiedOn
		{
			get => this._modifiedOn;
			set => this._modifiedOn = value ??
				throw new InvalidEntityException(ExceptionMessages.ModifiedOnNotNull);
		}
	}
}
