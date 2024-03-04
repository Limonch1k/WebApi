using GeneralObject.LinqExtension;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class SwaggerSchemaCustomFilter : ISchemaFilter
{
    public List<string> _key {get;set;}

    public SwaggerSchemaCustomFilter()
    {
        _key = new List<string>(){"BL"};
    }


    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var keys = new List<string>();

        foreach(var key in context.SchemaRepository.Schemas.Keys)
        {
            if (key.ContainsAny(_key.ToArray()))
            {
                keys.Add(key);
            }
        }
        foreach(var key in keys)
        {
            context.SchemaRepository.Schemas.Remove(key);
        }
    }
}