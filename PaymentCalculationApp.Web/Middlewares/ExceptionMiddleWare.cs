using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace PaymentCalculation.Middlewares
{
    public class ExceptionMiddleWare
    {
        private RequestDelegate Next { get; }


        public ExceptionMiddleWare(RequestDelegate next)
        {
            Next = next;
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (Exception ex)
            {

                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = ex.Message,
                    Instance = "",
                    Title = "Internal Server Error - something went wrong",
                    Type = "Error"

                };

                var problemDetailsJson = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(problemDetailsJson);
            }
        }

    }
}
