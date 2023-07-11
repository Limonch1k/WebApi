using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

public class ValidateGetByDateAttribute : ActionFilterAttribute
{

    public string _regexStartAt {get;set;}

    public string _regexEndAt {get;set;}

    public ValidateGetByDateAttribute(string regexStartAt, string regexEndAt)
    {
        _regexStartAt = regexStartAt;
        _regexEndAt = regexEndAt;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var queryList = context.HttpContext.Request.Query;
        if(queryList.Count != 2)
        {
            context.Result = new BadRequestObjectResult("Request must contain only two parameters; parameters must be startAt and endAt;");
        }

        if(!queryList.ContainsKey("startAt") && queryList.ContainsKey("endAt"))
        {
            context.Result = new BadRequestObjectResult("Request must be startAt and endAt");
        }

        DateTime startAt;
        DateTime endAt;

        if(DateTime.TryParseExact(queryList["startAt"], new[] { "yyyy-MM-dd","yyyy/MM/dd","yyyy.MM.dd"}, new CultureInfo("en-US"), DateTimeStyles.None, out startAt))
        {
            var a = context.HttpContext.Request.Query;
        }

        if(DateTime.TryParseExact(queryList["endAt"], new[] { "yyyy-MM-dd","yyyy/MM/dd","yyyy.MM.dd"}, new CultureInfo("en-US"), DateTimeStyles.None, out endAt))
        {

        }

    }
    public override void OnActionExecuted(ActionExecutedContext context)
    {
    }
}