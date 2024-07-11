using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace zms.Root.Api.FrontendApi.Swagger
{
    /// <summary>
    /// Фильтр наименований схем
    /// </summary>
    public class NamespaceSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema is null)
                throw new ArgumentNullException(nameof(schema));

            if (context is null)
                throw new ArgumentNullException(nameof(context));

            schema.Title = context.Type.Name;
        }
    }
}
