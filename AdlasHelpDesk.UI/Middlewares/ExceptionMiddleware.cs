using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using AdlasHelpDesk.Application.Results;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AdlasHelpDesk.UI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task<Result> HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //await context.Response.WriteAsync(JsonConvert.SerializeObject(new Result(new Meta()
            return  new Result(new Meta()
            {
                Code = context.Response.StatusCode,
                Message = "Internal Server Error.",
                MessageDetail = exception?.InnerException?.Message,
                IsSuccess = false

            });
        }
    }
}
