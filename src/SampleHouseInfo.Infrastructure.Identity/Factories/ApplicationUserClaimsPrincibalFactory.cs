using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SampleHouseInfo.Infrastructure.Identity.Models;
using System.Security.Claims;

namespace SampleHouseInfo.Infrastructure.Identity.Factories;

public class ApplicationUserClaimsPrincibalFactory : UserClaimsPrincipalFactory<ApplicationUser>
{
  public ApplicationUserClaimsPrincibalFactory
          (UserManager<ApplicationUser> userManager, 
           IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
  {
  }


  /// <summary>
  /// Generate the claims for a user.
  /// </summary>
  /// <param name="user">The user to create a <see cref="T:System.Security.Claims.ClaimsIdentity" /> from.</param>
  /// <returns>
  /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous creation operation, containing the created <see cref="T:System.Security.Claims.ClaimsIdentity" />.
  /// </returns>
  protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
  {
    var claims = await base.GenerateClaimsAsync(user);

    if(user.FirstName != null)
    {
      claims.AddClaim(new Claim(JwtClaimTypes.GivenName, user.FirstName));
    }

    if (user.LastName != null)
    {
      claims.AddClaim(new Claim(JwtClaimTypes.FamilyName, user.LastName));
    }

    return claims;
  }
}
