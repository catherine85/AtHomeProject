using AtHomeProject.Request;
using AtHomeProject.Response;
using AtHomeProject.Services;
using System.Net.Http;
using System.Threading.Tasks;

namespace AtHomeProject.Logic
{
    public class Json2Api : BaseApi
    {
        public Json2Api(IExternalApi external) : base(external) { }

        public override async Task<decimal> GetResultAsync(string url, OfferRequest offer)
        {
            var apiRequest = new Json2ApiRequest { Cartons = offer.Dimensions, Consignee = offer.SourceAddress, Consignor = offer.DestinationAddress };
            try
            {
                var result = _externalApi.GetJsonResultAsync(url, apiRequest);
                var parseResult = await DeserializeAsync<Api2Response>(await result);
                return parseResult.Amount;
            }
            catch (HttpRequestException)
            {

                return decimal.MaxValue;
            }

        }
    }
}
