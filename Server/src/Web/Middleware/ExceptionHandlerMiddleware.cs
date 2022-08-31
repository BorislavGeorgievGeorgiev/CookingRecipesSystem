using System.Net;

using CookingRecipesSystem.Application.Common.Exceptions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;

namespace CookingRecipesSystem.Web.Middleware
{
	public class ExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionHandlerMiddleware(RequestDelegate next)
				=> _next = next;

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			const string ApplicationJson = "application/json";

			var code = HttpStatusCode.InternalServerError;

			var result = string.Empty;

			switch (exception)
			{
				case ModelValidationException validationException:
					code = HttpStatusCode.BadRequest;
					result = JsonConvert.SerializeObject(validationException.Failures);
					break;
				case NotCreatedException _:
					code = HttpStatusCode.BadRequest;
					break;
				//TODO: Change to use correct exception like ModelValidationException !
				case ArgumentNullException argumentNullException:
					code = HttpStatusCode.BadRequest;
					result = JsonConvert.SerializeObject(argumentNullException.Message);
					break;
				case ArgumentException argumentException:
					code = HttpStatusCode.BadRequest;
					result = JsonConvert.SerializeObject(argumentException.Message);
					break;
				case NotFoundException _:
					code = HttpStatusCode.NotFound;
					break;
			}

			context.Response.ContentType = ApplicationJson;
			context.Response.StatusCode = (int)code;

			if (string.IsNullOrEmpty(result))
			{
				result = JsonConvert.SerializeObject(new { error = exception.Message });
			}

			return context.Response.WriteAsync(result);
		}
	}

	public static class CustomExceptionHandlerMiddlewareExtensions
	{
		public static IApplicationBuilder UseCustomExceptionHandler(
			this IApplicationBuilder builder)
				=> builder.UseMiddleware<ExceptionHandlerMiddleware>();
	}
}
