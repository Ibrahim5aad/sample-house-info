using Microsoft.OpenApi.Models;
using System.Reflection;

namespace SampleHouseInfo.API.Extensions;


/// <summary>
/// Class ServiceCollectionExtensions
/// </summary>
public static class ServiceCollectionExtensions
{

  /// <summary>
  /// Registers swagger to the DI container.
  /// </summary>
  /// <param name="services">The services.</param>
  /// <param name="configuration">The configuration.</param>
  /// <returns></returns>
  public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
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

      options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
      {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
          ClientCredentials = new OpenApiOAuthFlow
          {
            TokenUrl = new Uri($"{configuration["Authentication:Authority"]}/connect/token"),
            Scopes = { { "api", "API" } }
          }
        }
      });

      options.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
        { 
          new OpenApiSecurityScheme
          {

            Reference = new OpenApiReference
            {
              Type = ReferenceType.SecurityScheme,
              Id = "oauth2"
            }
          },
          new List<string>
          {
            "api"
          }
        }

      });

      var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
      var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
      options.IncludeXmlComments(xmlPath);

    });

  }

}
