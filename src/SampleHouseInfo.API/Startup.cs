using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SampleHouseInfo.API.Extensions;
using SampleHouseInfo.API.Middlewares;
using SampleHouseInfo.Application.Extensions;
using SampleHouseInfo.Domain.Settings;
using SampleHouseInfo.Infrastructure.Persistence.Extensions;
using Serilog;

namespace SampleHouseInfo.API;


/// <summary>
/// Class Startup
/// </summary>
public class Startup
{

  #region Fields

  private IConfiguration _config;

  #endregion

  #region Constructors

  /// <summary>
  /// Initializes a new instance of the <see cref="Startup"/> class.
  /// </summary>
  /// <param name="configuration">The configuration.</param>
  public Startup(IConfiguration configuration)
  {
    _config = configuration;
  }

  #endregion

  #region Methods

  /// <summary>
  /// Configures and registers the services required by the application.
  /// </summary>
  /// <param name="services">The services collection.</param>
  public void ConfigureServices(IServiceCollection services)
  {
    Log.Information("Registering services into the IoC container...");

    services.Configure<AppSettings>(_config);

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
              options.Authority = _config["Authentication:Authority"];

              options.TokenValidationParameters.ValidAudiences = new List<string>()
              {
                "SummaryApi",
                "RoomsApi"
              };

              options.TokenValidationParameters.ValidateAudience = true;
              options.TokenValidationParameters.ValidateIssuer = true;
              options.TokenValidationParameters.ValidateIssuerSigningKey = true;

            });

    services.AddAuthorization(options =>
    {
      options.AddPolicy("ApiScope", policyBuilder =>
      {
        policyBuilder.RequireAuthenticatedUser()
                      .RequireClaim("scope", "api");
      });

      options.AddPolicy("SummaryApiScope", policyBuilder =>
      {
        policyBuilder.RequireAuthenticatedUser()
                      .RequireClaim("scope", "summaryapi");
      });

    });

    services.AddControllers();

    services.AddCors(options =>
    {
      options.AddDefaultPolicy(builder =>
      {
        builder.AllowAnyOrigin()
                .AllowAnyMethod()
                 .AllowAnyHeader();
      });
    });

    services.AddSwagger(_config);

    services.RegisterPersistenceLayerServices();

    services.RegisterApplicationLayerServices();

    Log.Information("services registered successfully.");

  }


  /// <summary>
  /// Configures the http request pipeline.
  /// </summary>
  /// <param name="app">The application builder.</param>
  /// <param name="environment">The web host environment.</param>
  public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
  {

    Log.Information("Configuring the request pipeline...");

    if (environment.IsDevelopment())
    {
      Log.Information("Configuring dev. environement specific middlwares...");

      app.UseDeveloperExceptionPage();
    }

    app.UseSwaggerAndSwaggerUI();

    app.UseCors();

    app.UseHttpsRedirection();

    app.UseRouting(); //route info should be available for authentication decision

    app.UseAuthentication();

    app.UseAuthorization();

    app.UseSerilogRequestLogging();

    app.UseMiddleware<ErrorHandlerMiddleware>();

    app.UseEndpoints(endpoints =>
    {
      endpoints.MapControllers();
    });

    Log.Information("Request pipeline configured successfully.");

  }

  #endregion

}
