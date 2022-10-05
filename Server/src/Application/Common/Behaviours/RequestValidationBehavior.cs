﻿
using CookingRecipesSystem.Application.Common.Exceptions;

using FluentValidation;

using MediatR;

namespace CookingRecipesSystem.Application.Common.Behaviours
{
	public class RequestValidationBehavior<TRequest, TResponse>
			: IPipelineBehavior<TRequest, TResponse>
			where TRequest : IRequest<TResponse>
	{
		private readonly IEnumerable<IValidator<TRequest>> _validators;

		public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
				=> _validators = validators;

		public async Task<TResponse> Handle(
			TRequest request,
			RequestHandlerDelegate<TResponse> next,
			CancellationToken cancellationToken)
		{
			var context = new ValidationContext<TRequest>(request);

			var failures =
					_validators
					.Select(v => v.Validate(context))
					.SelectMany(result => result.Errors)
					.Where(f => f != null)
					.ToList();

			if (failures.Count != 0)
			{
				throw new ModelValidationException(failures);
			}

			var response = await next();

			return response;
		}
	}
}
