using AtHomeProject.Request;
using AtHomeProject.Services;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace AtHomeProject.Logic
{
    public abstract class BaseApi
    {
        protected readonly IExternalApi _externalApi;

        public BaseApi(IExternalApi externalApi)
        {
            _externalApi = externalApi;

        }

        public virtual async Task<T> DeserializeAsync<T>(Stream result)
        {
            return await JsonSerializer.DeserializeAsync<T>(result);
        }

        public abstract Task<decimal> GetResultAsync(string url, OfferRequest offer);


    }
}
