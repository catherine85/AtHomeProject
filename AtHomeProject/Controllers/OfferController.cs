using AtHomeProject.Logic;
using AtHomeProject.Request;
using AtHomeProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace AtHomeProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OfferController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IExternalApi _externalApi;

        public OfferController(IConfiguration configuration, IExternalApi externalApi)
        {
            _configuration = configuration;
            _externalApi = externalApi;
        }

        [HttpPost]
        [Route("GetAsync")]
        public async Task<ActionResult<decimal>> GetAsync([FromBody] OfferRequest request)
        {
            var api1 = new Json1Api(_externalApi).GetResultAsync(_configuration.GetSection("Apijson1").Value, request);
            var api2 = new Json2Api(_externalApi).GetResultAsync(_configuration.GetSection("Apijson2").Value, request);
            var xmlApi = new XmlApi(_externalApi).GetResultAsync(_configuration.GetSection("ApiXML").Value, request);
            var result = Math.Min(Math.Min(await api1, await api2), await xmlApi);
            return result != decimal.MaxValue ? result : NotFound("Offer not Found");
        }
    }
}
