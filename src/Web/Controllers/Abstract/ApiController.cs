using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Controllers.Abstract
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ApiController : Controller
    {
        private readonly ILogger _logger;

        public ApiController(ILogger logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.ExceptionHandled && context.Exception != null)
            {
                _logger.LogError(new EventId(), exception: context.Exception, "Unhandled exception");
                context.Result = new ErrorResult(context.Exception);
                context.ExceptionHandled = true;
            }

            base.OnActionExecuted(context);
        }
    }
}
