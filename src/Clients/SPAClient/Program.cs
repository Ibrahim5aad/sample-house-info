using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SPAClient;
using SPAClient.Handlers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("Api", client =>
{
  client.BaseAddress = new Uri("https://api:7001/api/");
}).AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

builder.Services.AddOidcAuthentication(options =>
{
  options.ProviderOptions.Authority = builder.Configuration["Authentication:Authority"];
  options.ProviderOptions.ClientId = builder.Configuration["Authentication:ClientId"];
  options.ProviderOptions.ResponseType = "code";
  options.ProviderOptions.DefaultScopes.Add("summaryapi");

});

builder.Services.AddScoped<ApiAuthorizationMessageHandler>();

await builder.Build().RunAsync();
