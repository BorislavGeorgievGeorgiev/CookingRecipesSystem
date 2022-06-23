using System.Reflection;

namespace CookingRecipesSystem.Domain.Common
{
	public abstract class ValueObject
	{
		// Source: https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/implement-value-objects 

		private readonly BindingFlags _bindingFlags =
			BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

		protected static bool EqualOperator(ValueObject left, ValueObject right)
		{
			if (left is null ^ right is null)
			{
				return false;
			}

			return left?.Equals(right) != false;
		}

		protected static bool NotEqualOperator(ValueObject left, ValueObject right)
		{
			return !EqualOperator(left, right);
		}

		protected IEnumerable<object> GetEqualityComponents()
		{
			return this.GetFields();
		}

		public override bool Equals(object? obj)
		{
			if (obj == null || obj.GetType() != this.GetType())
			{
				return false;
			}

			var other = (ValueObject)obj;

			return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
		}

		public static bool operator ==(ValueObject one, ValueObject two)
		{
			return EqualOperator(one, two);
		}

		public static bool operator !=(ValueObject one, ValueObject two)
		{
			return NotEqualOperator(one, two);
		}

		public override int GetHashCode()
		{
			return this.GetEqualityComponents()
					.Select(x => x != null ? x.GetHashCode() : 0)
					.Aggregate((x, y) => x ^ y);
		}

		private IEnumerable<FieldInfo> GetFields()
		{
			var type = this.GetType();

			var fields = new List<FieldInfo>();

			while (type != typeof(object) && type != null)
			{
				fields.AddRange(type.GetFields(this._bindingFlags));

				type = type.BaseType!;
			}

			return fields;
		}
	}
}
