using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SampleHouseInfo.Infrastructure.Identity.Models;

namespace SampleHouseInfo.Infrastructure.Identity.Data.Seeds
{
  /// <summary>
  /// 
  /// </summary>
  public class ApplicationDbContextSeed
  { 

    private readonly static IPasswordHasher<ApplicationUser> _passwordHasher =
                                                 new PasswordHasher<ApplicationUser>();


    public async static Task SeedAsync(IServiceProvider services, ApplicationDbContext context, int? retry = 0)
    {
      int retryForAvaiability = retry ?? 0;

      try
      { 
        if (!await context.Roles.AnyAsync())
        {
          context.Roles.AddRange(GetDefaultRoles());
          await context.SaveChangesAsync();
        }
        if (!await context.Users.AnyAsync())
        {
          context.Users.AddRange(GetDefaultUser());
          await context.SaveChangesAsync();
          await AssignRolesToUser(services, "admin@sanveo.com", new[] { Roles.SuperAdmin });
        }
      }
      catch (Exception)
      {
        if (retryForAvaiability < 10)
        {
          retryForAvaiability++;

          await SeedAsync(services, context, retryForAvaiability);
        }
      }
    }


    /// <summary>
    /// Gets the default user.
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<ApplicationUser> GetDefaultUser()
    {
      var user =
      new ApplicationUser()
      {
        Email = "admin@samplehouseinfo.com",
        NormalizedEmail = "ADMIN@SAMPLEHOUSEINFO.COM",
        UserName = "admin",
        NormalizedUserName = "ADMIN",
        FirstName = "Super",
        LastName = "Admin",
        PhoneNumber = "1234567890",
        SecurityStamp = Guid.NewGuid().ToString("D"),
        EmailConfirmed = true
      };

      user.PasswordHash = _passwordHasher.HashPassword(user, "P@ssw0rd");

      return new List<ApplicationUser> { user };
    }


    /// <summary>
    /// Gets the default roles.
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<IdentityRole> GetDefaultRoles()
    {
      return new List<IdentityRole>()
      {
                new IdentityRole()
                {
                    Name = Roles.SuperAdmin,
                    NormalizedName = Roles.SuperAdmin.ToUpper()
                },
                new IdentityRole()
                {
                    Name = Roles.CompanyAdmin,
                    NormalizedName = Roles.CompanyAdmin.ToUpper()
                },
                new IdentityRole()
                {
                    Name = Roles.StandardUser,
                    NormalizedName = Roles.StandardUser.ToUpper()
                }
            };
    }


    /// <summary>
    /// Assigns the roles to user.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="userEmail">The user email.</param>
    /// <param name="roles">The roles.</param>
    /// <returns></returns>
    public static async Task<IdentityResult> AssignRolesToUser
          (IServiceProvider services, string userEmail, string[] roles)
    {
      UserManager<ApplicationUser> _userManager =
              services.GetService<UserManager<ApplicationUser>>()!;

      ApplicationUser user = await _userManager.FindByEmailAsync(userEmail);
      return await _userManager.AddToRolesAsync(user, roles);
    }

  }
}
