namespace CookingRecipesSystem.Domain.Common
{
	public abstract class Entity<TKey> : IEntity<TKey>
		where TKey : struct
	{
		public virtual TKey Id { get; protected set; } = default;

		public override bool Equals(object? obj)
		{
			if (obj is not Entity<TKey> other)
			{
				return false;
			}

			if (ReferenceEquals(this, other))
			{
				return true;
			}

			if (this.GetType() != other.GetType())
			{
				return false;
			}

			if (this.Id.Equals(default) || other.Id.Equals(default))
			{
				return false;
			}

			return this.Id.Equals(other.Id);
		}

		public static bool operator ==(Entity<TKey>? first, Entity<TKey>? second)
		{
			if (first is null && second is null)
			{
				return true;
			}

			if (first is null || second is null)
			{
				return false;
			}

			return first.Equals(second);
		}

		public static bool operator !=(Entity<TKey>? first, Entity<TKey>? second)
			=> !(first == second);

		public override int GetHashCode()
			=> (this.GetType().ToString() + this.Id).GetHashCode() ^ 37;

	}
}
