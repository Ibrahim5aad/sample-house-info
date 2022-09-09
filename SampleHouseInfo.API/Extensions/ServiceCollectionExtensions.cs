using Microsoft.OpenApi.Models;
using System.Reflection;

namespace SampleHouseInfo.API.Extensions
{
  /// <summary>
  /// Class ServiceCollectionExtensions
  /// </summary>
  public static class ServiceCollectionExtensions
  {

    /// <summary>
    /// Registers swagger to the DI container.
    /// </summary>
    /// <param name="services">The services.</param>
    public static void AddSwagger(this IServiceCollection services)
    {

      services.AddSwaggerGen(options =>
      {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
          Version = "v1",
          Title = "SampleHouseInfo",
          Contact = new OpenApiContact
          {
            Name = "Ibrahim Saad",
            Email = "ibrahimsaad419@gmail.com", 
          }
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);

      });

    }

  }
}
