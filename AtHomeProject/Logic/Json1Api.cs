using AtHomeProject.Request;
using AtHomeProject.Response;
using AtHomeProject.Services;
using System.Net.Http;
using System.Threading.Tasks;

namespace AtHomeProject.Logic
{
    public class Json1Api : BaseApi
    {
        public Json1Api(IExternalApi external) : base(external) { }

        public override async Task<decimal> GetResultAsync(string url, OfferRequest offer)
        {
            var apiRequest = new Json1ApiRequest { ContactAddress = offer.SourceAddress, WareHouseAddress = offer.DestinationAddress, PackageDimensions = offer.Dimensions };
            try
            {
                var result = _externalApi.GetJsonResultAsync(url, apiRequest);
                var parseResult = await DeserializeAsync<Api1Response>(await result);
                return parseResult.Total;
            }
            catch (HttpRequestException)
            {

                return decimal.MaxValue;
            }

        }
    }
}
