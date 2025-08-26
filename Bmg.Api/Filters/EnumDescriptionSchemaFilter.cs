using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;
using System.Reflection;

namespace Bmg.Api.Filters;

public class EnumDescriptionSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            var enumType = context.Type;
            var enumDescriptions = new List<OpenApiString>();

            foreach (var value in Enum.GetValues(enumType))
            {
                var intValue = (int)value!;
                var name = Enum.GetName(enumType, value)!;
                var member = enumType.GetMember(name).First();
                var descriptionAttr = member.GetCustomAttribute<DescriptionAttribute>();
                var description = descriptionAttr?.Description ?? name;

                enumDescriptions.Add(new OpenApiString($"{intValue} - {description}"));
            }

            schema.Enum.Clear();
            
            foreach (var item in enumDescriptions)
                schema.Enum.Add(item);
        }
    }
}
