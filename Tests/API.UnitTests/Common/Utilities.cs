using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.IntegrationTests.Common
{
  internal class Utilities
  {
    public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
    {
      var stringResponse = await response.Content.ReadAsStringAsync();

      var result = JsonConvert.DeserializeObject<T>(stringResponse);

      return result;
    }
  }
}
