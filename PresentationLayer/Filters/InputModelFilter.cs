using AutoMapper;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace api_fact_weather_by_city.Filters
{
    public class InputModelFilter<JsonModel,PLModel> : Attribute, IActionFilter
    {
        private IMapper _mapper { get; set; }

        public InputModelFilter(IMapper mapper) 
        {
            _mapper = mapper;
        }


        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            using var reader = new StreamReader(context.HttpContext.Request.Body);
            var body = reader.ReadToEnd();
            var model = JsonSerializer.Deserialize<List<JsonModel>>(body);

            var models_pl = _mapper.Map<List<PLModel>>(model);

            context.ActionArguments["models"] = models_pl;
        }
    }
}
