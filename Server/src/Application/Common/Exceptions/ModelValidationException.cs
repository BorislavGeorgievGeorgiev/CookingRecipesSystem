﻿using CookingRecipesSystem.Domain.Common.Constants;

using FluentValidation.Results;

namespace CookingRecipesSystem.Application.Common.Exceptions
{
	public class ModelValidationException : Exception
	{
		public ModelValidationException()
				: base(ExceptionMessages.ModelValidationFailures)
				=> this.Failures = new Dictionary<string, string[]>();

		public ModelValidationException(List<ValidationFailure> failures)
				: this()
		{
			var failureGroups = failures
					.GroupBy(e => e.PropertyName, e => e.ErrorMessage);

			foreach (var failureGroup in failureGroups)
			{
				var propertyName = failureGroup.Key;
				var propertyFailures = failureGroup.ToArray();

				this.Failures.Add(propertyName, propertyFailures);
			}
		}

		public IDictionary<string, string[]> Failures { get; }
	}
}
