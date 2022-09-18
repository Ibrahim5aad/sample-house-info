using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SampleHouseInfo.Infrastructure.Identity.Models;

namespace SampleHouseInfo.Infrastructure.Identity.Data;


/// <summary>
///   <br />
/// </summary>
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{

  /// <summary>
  /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
  /// </summary>
  /// <param name="options">The options.</param>
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
  {

  }

}
