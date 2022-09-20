using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using WebAppClient.Models.Dtos;

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
    /// Gets or sets the rooms.
    /// </summary>
    /// <value>
    /// The rooms.
    /// </value>
    public IEnumerable<RoomDto>? Rooms { get; set; }


    /// <summary>
    /// Called on [get] requests.
    /// </summary>
    public async Task OnGetAsync()
    {
      using var httpClient = HttpClientFactory.CreateClient("Api");

      httpClient.DefaultRequestHeaders.Authorization
        = new AuthenticationHeaderValue(scheme: "Bearer", await HttpContext.GetTokenAsync("access_token"));

      Rooms = await httpClient.GetFromJsonAsync<IEnumerable<RoomDto>>("rooms");


    }
  }
}
