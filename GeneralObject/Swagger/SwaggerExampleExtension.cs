using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Xml;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;

namespace GeneralObject.Swagger
{
    public static class ExmpleOperationFilterExtensions
    {
        public static void UseExampleFilter([NotNull] this SwaggerGenOptions app, [NotNull] Action<ExampleOperationFilterConfig> configure)
        {
            var configuration = new ExampleOperationFilterConfig();
            configure(configuration);
            app.OperationFilter<ExampleOperationFilter>(configuration);
        }
    }


    public class ExampleOperationFilterConfig
    {
        private readonly IDictionary<string, Dictionary<string,string>> invocationExamples = new Dictionary<string, Dictionary<string,string>>();

        [Pure]
        private static Dictionary<string, object> BuildParameterValuesFromExpression([NotNull] MethodCallExpression call)
        {
            var parameterValues = new Dictionary<string, object>();
            var parameters = call.Method.GetParameters();
            if (parameters.Length > 0)
            {
                for (int index = 0; index < parameters.Length; index++)
                {
                    var parameterExpression = call.Arguments[index];
                    var value = Expression.Lambda(parameterExpression)
                                          .Compile()
                                          .DynamicInvoke();
                    parameterValues.Add(parameters[index].Name, value);
                }
            }
            return parameterValues;
        }

        public void DefineExample<TController>([NotNull] Expression<Action<TController>> sampleInvocation)
        {
            var methodCall= (MethodCallExpression) sampleInvocation.Body;
            var actionName = methodCall.Method.Name;
            var controllerName = typeof(TController).Name.Replace("Controller", "");
            var key = GetActionKey(controllerName, actionName);
            this.invocationExamples[key] = BuildParameterValuesFromExpression(methodCall)
                .ToDictionary(x=>x.Key, x=> JsonConvert.SerializeObject(x.Value, Newtonsoft.Json.Formatting.Indented));
        }

        [Pure]
        private static string GetActionKey(string controllerName, string actionName)
        {
            return $"{controllerName}_{actionName}";
        }

        [Pure]
        public bool TryGetActionParametersExamples([NotNull] ControllerActionDescriptor c, out Dictionary<string, string> parametersValues)
        {
            var actionKey = GetActionKey(c.ControllerName, c.ActionName);
            return this.invocationExamples.TryGetValue(actionKey, out parametersValues);
        }
    }


    public class ExampleOperationFilter: IOperationFilter
    {
        private readonly ExampleOperationFilterConfig config;

        /// <inheritdoc />
        public ExampleOperationFilter(ExampleOperationFilterConfig config)
        {
            this.config = config;
        }

        /// <inheritdoc />
        public void Apply([NotNull] OpenApiOperation operation, [NotNull] OperationFilterContext context)
        {
            /*if (context.ApiDescription.ActionDescriptor is ControllerActionDescriptor actionDescriptor && operation.Parameters != null)
            {
                if (this.config.TryGetActionParametersExamples(actionDescriptor, out var parametersValues))
                {
                    foreach (var parameter in operation.Parameters.OfType<BodyParameter>())
                    {
                        if (parametersValues.TryGetValue(parameter.Name, out var parameterValue))
                        {
                            parameter.Extensions.Add("default", parameterValue);
                        }
                    }
                }
            }*/
        }
    }
}
    
