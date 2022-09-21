using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace SPAClient.Handlers
{
  public class ApiAuthorizationMessageHandler : AuthorizationMessageHandler
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ApiAuthorizationMessageHandler"/> class.
    /// </summary>
    /// <param name="provider">The <see cref="T:Microsoft.AspNetCore.Components.WebAssembly.Authentication.IAccessTokenProvider" /> to use for provisioning tokens.</param>
    /// <param name="navigation">The <see cref="T:Microsoft.AspNetCore.Components.NavigationManager" /> to use for performing redirections.</param>
    public ApiAuthorizationMessageHandler
      (IAccessTokenProvider provider, NavigationManager navigation) : base(provider, navigation)
    {
      ConfigureHandler(new List<string>
      {
        "https://api:7001"
      }, new List<string>
      {
        "summaryapi",
      });

    }
  }
}
