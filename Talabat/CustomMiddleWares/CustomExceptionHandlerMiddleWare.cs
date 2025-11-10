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
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Something Went Wrong");
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                //httpContext.Response.ContentType = "application/json";
                var response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    ErrorMessage=ex.Message

                };
                //var responseToReturn = JsonSerializer.Serialize(response);
                await httpContext.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
