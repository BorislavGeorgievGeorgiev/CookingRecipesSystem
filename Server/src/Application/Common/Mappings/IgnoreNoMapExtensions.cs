using System.ComponentModel;

using AutoMapper;

namespace CookingRecipesSystem.Application.Common.Mappings
{
	public static class IgnoreNoMapExtensions
	{
		public static IMappingExpression<TSource, TDestination> IgnoreNoMap<TSource, TDestination>(
				this IMappingExpression<TSource, TDestination> expression)
		{
			var sourceType = typeof(TSource);

			foreach (var property in sourceType.GetProperties())
			{
				PropertyDescriptor descriptor = TypeDescriptor.GetProperties(sourceType)[property.Name]!;
				NoMapAttribute attribute = (NoMapAttribute)descriptor.Attributes[typeof(NoMapAttribute)]!;

				if (attribute != null)
				{
					expression.ForMember(property.Name, opt => opt.Ignore());
				}
			}
			return expression;
		}

		public static IMappingExpression IgnoreNoMap(
				this IMappingExpression expression, Type source, Type dest)
		{
			var sourceType = source;

			foreach (var property in sourceType.GetProperties())
			{
				PropertyDescriptor descriptor = TypeDescriptor.GetProperties(sourceType)[property.Name]!;
				NoMapAttribute attribute = (NoMapAttribute)descriptor.Attributes[typeof(NoMapAttribute)]!;

				if (attribute != null)
				{
					expression.ForMember(property.Name, opt => opt.Ignore());
				}
			}
			return expression;
		}
	}
}
