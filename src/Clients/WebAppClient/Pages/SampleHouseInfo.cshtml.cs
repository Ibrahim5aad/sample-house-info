using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace WebAppClient.Pages
{
  [Authorize]
  public class SampleHouseInfoModel : PageModel
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="SampleHouseInfoModel"/> class.
    /// </summary>
    /// <param name="httpClientFactory">The HTTP client factory.</param>
    public SampleHouseInfoModel(IHttpClientFactory httpClientFactory)
    {
      HttpClientFactory = httpClientFactory;
    }


    /// <summary>
    /// Gets the HTTP client factory.
    /// </summary>
    /// <value>
    /// The HTTP client factory.
    /// </value>
    private IHttpClientFactory HttpClientFactory { get; }


    /// <summary>
    /// Gets or sets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    public string? Data { get; set; }


    /// <summary>
    /// Called on [get] requests.
    /// </summary>
    public async Task OnGetAsync()
    {
      using var httpClient = HttpClientFactory.CreateClient();

      httpClient.BaseAddress = new Uri("https://api:7001/api/");

      httpClient.DefaultRequestHeaders.Authorization
        = new AuthenticationHeaderValue(scheme: "Bearer", await HttpContext.GetTokenAsync("access_token"));

      Data = await httpClient.GetStringAsync("rooms");

    }
  }
}
