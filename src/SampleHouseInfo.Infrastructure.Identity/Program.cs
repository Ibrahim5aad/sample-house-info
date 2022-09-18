using Duende.IdentityServer.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SampleHouseInfo.Infrastructure.Identity.Data;
using SampleHouseInfo.Infrastructure.Identity.Data.Seeds;
using SampleHouseInfo.Infrastructure.Identity.Factories;
using SampleHouseInfo.Infrastructure.Identity.Models;

var builder = WebApplication.CreateBuilder(args);

#region Services Registeration


builder.Services.AddDbContext<ApplicationDbContext>((services, builder) =>
{
  builder.UseNpgsql(
    services.GetRequiredService<IConfiguration>().GetConnectionString("Identity"),
    builder =>
    {
      builder.MigrationsAssembly(typeof(Program).Assembly.GetName().Name);
    });
});


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincibalFactory>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddIdentityServer()
                .AddAspNetIdentity<ApplicationUser>()
                .AddConfigurationStore(configurationStoreOptions =>
                {
                  configurationStoreOptions.ResolveDbContextOptions = ResolveIdentityServerDbContext;
                })
                .AddOperationalStore(operaionalStoreOptions =>
                {
                  operaionalStoreOptions.ResolveDbContextOptions = ResolveIdentityServerDbContext;
                });


builder.Services.AddRazorPages();

#endregion

#region Request Pipeline

var app = builder.Build();

//Build the request pipeline
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
app.MapRazorPages();

#endregion

#region Db Migrations and Seeds

using var scope = app.Services.CreateScope();
var appDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
var configDbContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
var persistedGrantsDbContext = scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();

await appDbContext.Database.MigrateAsync();
await configDbContext.Database.MigrateAsync();
await persistedGrantsDbContext.Database.MigrateAsync();

await ApplicationDbContextSeed.SeedAsync(app.Services, appDbContext, 2);
await ConfigurationDbContextSeed.SeedAsync(app.Services, configDbContext, 2);

#endregion


app.Run();


void ResolveIdentityServerDbContext(IServiceProvider services, DbContextOptionsBuilder builder)
{
  builder.UseNpgsql(
          services.GetRequiredService<IConfiguration>().GetConnectionString("IdentityServer"),
          builder =>
          {
            builder.MigrationsAssembly(typeof(Program).Assembly.GetName().Name);
          });
};