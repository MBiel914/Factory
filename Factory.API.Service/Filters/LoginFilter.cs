using Factory.API.Service.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Factory.API.Service.Filters
{
    public class LoginFilter : IActionFilter
    {
        private const string emailField = "Email";
        private readonly ILogger<LoginFilter> _logger;

        public LoginFilter(ILogger<LoginFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var loginObject = context.ActionArguments.FirstOrDefault().Value;

            var loginPropsType = loginObject.GetType().GetProperties();
            var loginProps = loginObject.GetType().GetProperties()
                .Where(prop => prop.CanRead)
                .ToDictionary(prop => prop.Name);

            foreach (var loginPropType in loginPropsType)
            {
                if (loginProps.TryGetValue(loginPropType.Name, out var sourceProperty)
                    && loginPropType.Name == emailField)
                {
                    _logger.LogInformation($"!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    _logger.LogInformation($"User: {sourceProperty.GetValue(loginObject)}, try to login.");
                }
            }
        }
    }
}
