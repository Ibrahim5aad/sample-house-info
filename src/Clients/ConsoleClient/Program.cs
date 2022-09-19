using IdentityModel.Client;
using System.Net.Http.Headers;

using var identityServerHttpClient = new HttpClient
{
  BaseAddress = new Uri(Environment.GetEnvironmentVariable("AUTHENTICATION__AUTHORITY")!)

};


var discoveryDocument = await identityServerHttpClient.GetDiscoveryDocumentAsync();

Console.WriteLine(discoveryDocument.TokenEndpoint);

var tokenResponse = await identityServerHttpClient.RequestClientCredentialsTokenAsync
                        (new ClientCredentialsTokenRequest
                        {
                          Address = discoveryDocument.TokenEndpoint,
                          ClientId = Environment.GetEnvironmentVariable("AUTHENTICATION__CLIENTID")!,
                          ClientSecret = Environment.GetEnvironmentVariable("AUTHENTICATION__CLIENTSECRET")!,
                          Scope = "api",
                        });

Console.WriteLine(tokenResponse.AccessToken);

using var httpClient = new HttpClient()
{
  DefaultRequestHeaders =
  {
    Authorization = new AuthenticationHeaderValue(scheme: "Bearer", tokenResponse.AccessToken)
  },
  BaseAddress = new Uri("https://api:7001/api/")
};

Console.WriteLine(await httpClient.GetStringAsync("rooms"));