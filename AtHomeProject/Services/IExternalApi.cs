using System.IO;
using System.Threading.Tasks;

namespace AtHomeProject.Services
{
    public interface IExternalApi
    {
        Task<Stream> GetJsonResultAsync<T>(string companyApiPath, T offerRequest);

        Task<Stream> GetXMLResultAsync<T>(string companyApiPath, T offerRequest);
    }
}