using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers.Abstract
{
    public sealed class ErrorResult(Exception exception) : IActionResult
    {
        public string Message { get; } = exception.Message;
        public HttpStatusCode HttpStatusCode { get; } = exception switch
        {
            UnauthorizedAccessException => HttpStatusCode.Unauthorized,
            AlreadyExistsException => HttpStatusCode.Conflict,
            NotFoundException => HttpStatusCode.NotFound,
            _ => HttpStatusCode.InternalServerError,
        };

        public Task ExecuteResultAsync(ActionContext context)
        {
            var result = new JsonResult(new Error(Message))
            {
                StatusCode = (int)HttpStatusCode
            };

            return result.ExecuteResultAsync(context);
        }
    }

    public sealed record Error(string Message);
}
