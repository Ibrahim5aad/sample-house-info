using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using static Duende.IdentityServer.IdentityServerConstants;

namespace SampleHouseInfo.Infrastructure.Identity.Data.Seeds
{
  /// <summary>
  /// Class ConfigurationDbContextSeed
  /// </summary>
  public class ConfigurationDbContextSeed
  {

    public async static Task SeedAsync(IServiceProvider services, ConfigurationDbContext context, int? retry = 0)
    {
      int retryForAvaiability = retry ?? 0;


      try
      {
        if (!await context.ApiResources.AnyAsync())
        {
          await context.ApiResources.AddRangeAsync
                  (GetApiResources().Select(r => r.ToEntity()));

          await context.SaveChangesAsync();
        }

        if (!await context.ApiScopes.AnyAsync())
        {
          await context.ApiScopes.AddRangeAsync
                  (GetApiScopes().Select(r => r.ToEntity()));

          await context.SaveChangesAsync();
        }

        if (!await context.Clients.AnyAsync())
        {
          await context.Clients.AddRangeAsync
                  (GetClients().Select(r => r.ToEntity()));

          await context.SaveChangesAsync();
        }

        if (!await context.IdentityResources.AnyAsync())
        {
          await context.IdentityResources.AddRangeAsync
                  (GetIdentityResources().Select(r => r.ToEntity()));

          await context.SaveChangesAsync();
        }

      }
      catch (Exception ex)
      {
        if (retryForAvaiability < 10)
        {
          retryForAvaiability++;

          await SeedAsync(services, context, retryForAvaiability);
        }
      }
    }


    /// <summary>
    /// Gets the identity resources.
    /// </summary>
    /// <returns></returns>
    private static List<IdentityResource> GetIdentityResources()
    {
      return new List<IdentityResource>
      {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResources.Email(),
      };
    }


    /// <summary>
    /// Gets the API scopes.
    /// </summary>
    /// <returns></returns>
    private static List<ApiScope> GetApiScopes()
    {
      return new List<ApiScope>
      {
        new ApiScope
        {
          Name = "api",
          DisplayName = "API"
        }
      };
    }


    /// <summary>
    /// Gets the identity server clients.
    /// </summary>
    /// <returns></returns>
    private static List<Client> GetClients()
    {
      return new List<Client>
      {
        new Client
        {
          ClientId = Guid.NewGuid().ToString(),
          ClientSecrets = new List<Secret>{new ("secret".Sha512())},
          ClientName = "Console Application",
          AllowedScopes = new List<string> { "api" },
          AllowedGrantTypes = GrantTypes.ClientCredentials,
          AllowedCorsOrigins = new List<string> { "https://api:7001" }

        },
        new Client
        {
          ClientId = Guid.NewGuid().ToString(),
          ClientSecrets = new List<Secret>{new ("secret".Sha512())},
          ClientName = "Web Application",
          AllowedGrantTypes = GrantTypes.Code,
          AllowedScopes = new List<string>
          {
            StandardScopes.OpenId,
            StandardScopes.Profile,
            StandardScopes.Email,
            "api"
          },
          RedirectUris = new List<string>{ "https://webapplication:7002/signin-oidc" },
          PostLogoutRedirectUris = new List<string>{ "https://webapplication:7002/signout-callback-oidc" }
        },
        new Client
        {
          ClientId = Guid.NewGuid().ToString(),
          RequireClientSecret = false,
          ClientName = "SPA",
          AllowedGrantTypes = GrantTypes.Code,
          AllowedScopes = new List<string>
          {
            StandardScopes.OpenId,
            StandardScopes.Profile,
            StandardScopes.Email,
            "api"
          },
          AllowedCorsOrigins = new List<string> { "https://singlepageapplication:7003" },
          RedirectUris = new List<string>{ "https://singlepageapplication:7003/authentication/login-callback" },
          PostLogoutRedirectUris = new List<string>{ "https://singlepageapplication:7003/authentication/logout-callback" }
        },
      };
    }


    /// <summary>
    /// Gets the API resources.
    /// </summary>
    /// <returns></returns>
    private static List<ApiResource> GetApiResources()
    {
      return new List<ApiResource>
      {
        new ApiResource
        {
          Name = Guid.NewGuid().ToString(),
          DisplayName = "API",
          Scopes = new List<string>{ "api" }
        }
      };
    }

  }
}
