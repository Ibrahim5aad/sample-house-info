namespace SampleHouseInfo.API.Extensions
{
  public static class ApplicationBuilderExtensions
  {

    /// <summary>
    /// Adds the swagger and swagger UI middlewares to the request pipeline.
    /// </summary>
    /// <param name="app">The application.</param>
    public static void UseSwaggerAndSwaggerUI(this IApplicationBuilder app)
    {

      app.UseSwagger();
      app.UseSwaggerUI(options =>
      {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", 
            typeof(ApplicationBuilderExtensions).Assembly.FullName);
      });

    }

  }
}
