using AtHomeProject.Request;
using AtHomeProject.Services;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AtHomeProject.Logic
{
    public class XmlApi
    {
        private readonly IExternalApi _externalApi;

        public XmlApi(IExternalApi externalApi)
        {
            _externalApi = externalApi;
        }

        public async Task<decimal> GetResultAsync(string url, OfferRequest offer)
        {
            var apiRequest = new XmlApiRequest { Destination = offer.DestinationAddress, Source = offer.SourceAddress, Packages = offer.Dimensions };
            try
            {
                var result = await _externalApi.GetXMLResultAsync(url, apiRequest);

                return DeserializeXml(result);
            }
            catch (HttpRequestException)
            {

                return decimal.MaxValue;
            }


        }

        public decimal DeserializeXml(Stream result)
        {
            StreamReader readStream = new(result);
            var postresult = readStream.ReadToEnd();
            var xml = System.Xml.Linq.XElement.Parse(postresult);
            return decimal.Parse(xml.Element("quote").FirstAttribute.Value);
        }
    }
}
