using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace API.IntegrationTests.Common;


/// <summary>
/// Class CustomWebApplicationFactory
/// </summary>
/// <typeparam name="TStartup">The type of the startup.</typeparam>
/// <seealso cref="Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory&lt;TStartup&gt;" />
public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{

  /// <summary>
  /// Gives a fixture an opportunity to configure the application before it gets built.
  /// </summary>
  /// <param name="builder">The <see cref="T:Microsoft.AspNetCore.Hosting.IWebHostBuilder" /> for the application.</param>
  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder
        .ConfigureServices(services =>
        {
          // Ideally we would remove from the IoC container
          // some of the registered serivces and configure
          // other test services
        })
        .UseEnvironment("Development"); // hould have testing env. as well
  }

}