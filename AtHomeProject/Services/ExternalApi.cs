using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AtHomeProject.Services
{
    public class ExternalApi : IExternalApi
    {

        public async Task<Stream> GetJsonResultAsync<T>(string companyApiPath, T offerRequest)
        {
            using (HttpClient client = new())
            {
                var response = await client.PostAsJsonAsync<T>(companyApiPath, offerRequest);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStreamAsync();
            }
        }

        public async Task<Stream> GetXMLResultAsync<T>(string companyApiPath, T offerRequest)
        {
            using (HttpClient client = new())
            {
                var response = await client.PostAsXmlAsync(companyApiPath, offerRequest);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStreamAsync();
            }
        }
    }
}
