using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Http;
using Shared.ErrorModel;
using System.Text.Json;

namespace Talabat.CustomMiddleWares
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> _logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate Next,ILogger<CustomExceptionHandlerMiddleWare> Logger)
        {
            _next = Next;
            _logger = Logger;
        }

        public async Task InvokeAsync(HttpContext httpContext )
        {
            try
            {
                await _next.Invoke(httpContext);
                await HandleEndPointAsync(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something Went Wrong");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            //httpContext.Response.ContentType = "application/json";
            var response = new ErrorToReturn()
            {
                ErrorMessage = ex.Message

            };
            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException=> StatusCodes.Status401Unauthorized,
                BadReguestException badReguest => GetBadRequestErrors(badReguest,response),
                _ => StatusCodes.Status500InternalServerError
            };
            response.StatusCode = httpContext.Response.StatusCode;
            //var responseToReturn = JsonSerializer.Serialize(response);
            await httpContext.Response.WriteAsJsonAsync(response);
        }
        private static int GetBadRequestErrors(BadReguestException badReguest ,ErrorToReturn response)
        {
            response.Errors = badReguest.Errors;
            return StatusCodes.Status400BadRequest;
        }
        private static async Task HandleEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"End Point {httpContext.Request.Path} is Not Found"
                };
                await httpContext.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
