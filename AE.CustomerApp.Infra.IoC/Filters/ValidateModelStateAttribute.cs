using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AE.CustomerApp.Infra.IoC.Filters
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Validates Model state
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
                context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}
