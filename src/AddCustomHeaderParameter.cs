using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class AddCustomHeaderParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {


        if (operation.Parameters is null)
        {
            operation.Parameters = new List<OpenApiParameter>();
        }



        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "Token",
            In = ParameterLocation.Header,
            Description = "Token  xz8wM6zr2RfF18GBM0B5yrkoo",
            Required = true,
        });
    
    
    }
}