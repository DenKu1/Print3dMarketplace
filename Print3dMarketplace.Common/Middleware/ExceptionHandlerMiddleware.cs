using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace Print3dMarketplace.Common.Middleware;

public class ExceptionHandlerMiddleware
{
	private readonly RequestDelegate _next;

	public ExceptionHandlerMiddleware(RequestDelegate next)
	{
		_next = next;
	}

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

	private async Task HandleExceptionAsync(HttpContext context, Exception exception)
	{
		context.Response.ContentType = "application/json; charset=UTF-8";
		context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
		var content = new
		{
			context.Response.StatusCode,
			Message = $"Internal Server Error: {exception.Message}; InnerException: {exception.InnerException?.Message}"
		};
		await context.Response.WriteAsync(JsonSerializer.Serialize(content));
		await context.Response.Body.FlushAsync();
	}
}
