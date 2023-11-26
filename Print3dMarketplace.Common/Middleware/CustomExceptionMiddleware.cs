using System.Net;

namespace Print3dMarketplace.Common.Middleware;

/// <summary>
/// Abstract handler for all exceptions.
/// </summary>
public abstract class AbstractExceptionHandlerMiddleware
{

	private readonly RequestDelegate _next;

	/// <summary>
	/// Gets HTTP status code response and message to be returned to the caller.
	/// Use the ".Data" property to set the key of the messages if it's localized.
	/// </summary>
	/// <param name="exception">The actual exception</param>
	/// <returns>Tuple of HTTP status code and a message</returns>
	public abstract (HttpStatusCode code, string message) GetResponse(Exception exception);

	public AbstractExceptionHandlerMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task Invoke(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception exception)
		{
			var response = context.Response;
			response.ContentType = "application/json";

			var (status, message) = GetResponse(exception);
			response.StatusCode = (int)status;
			await response.WriteAsync(message);
		}
	}
}
