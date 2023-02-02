using Microsoft.AspNetCore.Mvc.Filters;

namespace DesignPatternsApi
{
    public sealed class ValidatorAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.ActionArguments.Values.Where(arg => arg.GetType() == (typeof(DemoItem)));

            var demoItem = request.FirstOrDefault() as DemoItem;

            if (demoItem.LocationName == "Athens")
            {
                Console.WriteLine("Hello Greek");
            }
        }
    }
}
