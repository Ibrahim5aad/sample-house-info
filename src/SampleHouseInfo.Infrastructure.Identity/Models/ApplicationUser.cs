using Microsoft.AspNetCore.Identity;

namespace SampleHouseInfo.Infrastructure.Identity.Models;

/// <summary>
/// Extends the default built-in IdentityUser.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Identity.IdentityUser" />
public class ApplicationUser : IdentityUser
{

  public string FirstName { get; set; }
  public string LastName { get; set; }

}
