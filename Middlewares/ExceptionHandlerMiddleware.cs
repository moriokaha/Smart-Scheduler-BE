using SmartScheduler.Data.DataTransferObjects;
using SmartScheduler.Exceptions;
using System.Net;

namespace SmartScheduler.Middlewares
{
    public class ExceptionHandlerMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
			try
			{
				await next(context);
			}
			catch (Exception exception)
			{
				string message;

				switch (exception)
				{
					case ClientException clientException:
						context.Response.StatusCode = (int)clientException.StatusCode;
						message = clientException.Message;
						break;
					default:
						context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        message = "Internal server error";
                        break;
				}

                context.Response.ContentType = "application/json";

				// TODO: Add logging

                await context.Response.WriteAsync(new ErrorDetails
				{
					Message = message,
					StatusCode = context.Response.StatusCode,

				}.ToString());
            }
        }
    }
}
