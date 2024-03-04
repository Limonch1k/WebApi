using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
 
namespace api_fact_weather_by_city.Filters
{
    public class AutorizationApiFilter : Attribute, IAuthorizationFilter
    {
        private string _page {get;set;}

        public AutorizationApiFilter(string page)
        {
            _page = page;
        }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User.Identity;
            if(user is not null && user.IsAuthenticated)
            {
                
                if (context.HttpContext.User.Claims.Where(c => c.Type.Equals(_page)).Select(c => c.Value).SingleOrDefault() is not null)
                {
                    return;
                }
                else
                {
                    context.Result = new ContentResult
                    {
                        Content = "dont have right",
                        StatusCode = 403
                    };
                }
            }
            else
            {
                context.Result = new ContentResult
                {
                    Content = "You should been autorizate first",
                    StatusCode = 401
                };
            }

        }
    }
}