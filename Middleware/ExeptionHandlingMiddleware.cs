namespace Prueba_Tecnica.Middleware
{
    using System.Net;
    using System.Text.Json;
    using Microsoft.AspNetCore.Http;
    using Prueba_Tecnica.Exceptions;

    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var statusCode = exception switch
            {
                BadRequestException => (int)HttpStatusCode.BadRequest,             
                UnauthorizedException => (int)HttpStatusCode.Unauthorized,         
                ForbiddenException => (int)HttpStatusCode.Forbidden,               
                NotFoundException => (int)HttpStatusCode.NotFound,                 
                ConflictException => (int)HttpStatusCode.Conflict,                 
                UnprocessableEntityException => (int)HttpStatusCode.UnprocessableEntity,
                ServiceUnavailableException => (int)HttpStatusCode.ServiceUnavailable, 
                _ => (int)HttpStatusCode.InternalServerError                       
            };

            response.StatusCode = statusCode;

            var result = JsonSerializer.Serialize(new
            {
                error = exception.Message,
                statusCode
            });

            return response.WriteAsync(result);
        }
    }
}
