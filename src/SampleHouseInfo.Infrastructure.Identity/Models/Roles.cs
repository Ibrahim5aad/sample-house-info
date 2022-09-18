namespace SampleHouseInfo.Infrastructure.Identity.Models
{
  public record class Roles
  {
    public const string SuperAdmin = "Super Admin";
    public const string CompanyAdmin = "Company Admin";
    public const string StandardUser = "Standard User";
  }
}
