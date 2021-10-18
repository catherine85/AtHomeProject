using AtHomeProject.Controllers;
using AtHomeProject.Request;
using AtHomeProject.Response;
using AtHomeProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Net.Http;
using System.Text.Json;

namespace AtHomeProjectTests
{
    public class GetOfferTest

    {
        private Mock<IExternalApi> _apiCall;
        private OfferController _controller;
        private Mock<IConfiguration> _configuration;

        [SetUp]
        public void Setup()
        {
            _configuration = new Mock<IConfiguration>();
            _configuration.Setup(s => s.GetSection("Apijson1").Value).Returns("https://api1Url");
            _configuration.Setup(s => s.GetSection("Apijson2").Value).Returns("https://api2Url");
            _configuration.Setup(s => s.GetSection("ApiXML").Value).Returns("https://xmlApiUrl");
            _apiCall = new Mock<IExternalApi>(MockBehavior.Strict);
            _controller = new OfferController(_configuration.Object, _apiCall.Object);
        }

        [Test]
        public void GetTest()
        {
            var api2StreamResponse = TestHelper.GenerateStreamFromString(JsonSerializer.Serialize(new Api2Response { Amount = 5.6M }));
            var api1StreamResponse = TestHelper.GenerateStreamFromString(JsonSerializer.Serialize(new Api1Response { Total = 10.6M }));
            var xmlApiStreamResponse = TestHelper.GenerateStreamFromString("<xml><quote value='200'/></xml>");
            _apiCall.Setup(s => s.GetJsonResultAsync(It.Is<string>(g => g == @"https://api2Url"), It.IsAny<Json2ApiRequest>())).ReturnsAsync(api2StreamResponse);
            _apiCall.Setup(s => s.GetJsonResultAsync(It.Is<string>(g => g == @"https://api1Url"), It.IsAny<Json1ApiRequest>())).ReturnsAsync(api1StreamResponse);
            _apiCall.Setup(s => s.GetXMLResultAsync(It.Is<string>(g => g == @"https://xmlApiUrl"), It.IsAny<XmlApiRequest>())).ReturnsAsync(xmlApiStreamResponse);
            var dimensionsList = TestHelper.GetDimensionsList();
            var result = _controller.GetAsync(new AtHomeProject.Request.OfferRequest { DestinationAddress = "addressDestination", SourceAddress = "addressSource", Dimensions = dimensionsList });
            Assert.AreEqual(result.Result.Value, 5.6);
        }

        [Test]
        public async System.Threading.Tasks.Task Get_ExternalApi2_ThrowExceptionTestAsync()
        {
            var api2StreamResponse = TestHelper.GenerateStreamFromString(JsonSerializer.Serialize(new Api2Response { Amount = 5.6M }));
            var api1StreamResponse = TestHelper.GenerateStreamFromString(JsonSerializer.Serialize(new Api1Response { Total = 10.6M }));
            var xmlApiStreamResponse = TestHelper.GenerateStreamFromString("<xml><quote value='200'/></xml>");
            _apiCall.Setup(s => s.GetJsonResultAsync(It.Is<string>(g => g == @"https://api2Url"), It.IsAny<Json2ApiRequest>())).ThrowsAsync(new HttpRequestException());
            _apiCall.Setup(s => s.GetJsonResultAsync(It.Is<string>(g => g == @"https://api1Url"), It.IsAny<Json1ApiRequest>())).ReturnsAsync(api1StreamResponse);
            _apiCall.Setup(s => s.GetXMLResultAsync(It.Is<string>(g => g == @"https://xmlApiUrl"), It.IsAny<XmlApiRequest>())).ReturnsAsync(xmlApiStreamResponse);
            var dimensionsList = TestHelper.GetDimensionsList();
            var result = _controller.GetAsync(new AtHomeProject.Request.OfferRequest { DestinationAddress = "addressDestination", SourceAddress = "addressSource", Dimensions = dimensionsList });
            Assert.AreEqual(10.6, (await result).Value);
        }

        [Test]
        public async System.Threading.Tasks.Task Get_ExternalApi1_ThrowExceptionTestAsync()
        {
            var api2StreamResponse = TestHelper.GenerateStreamFromString(JsonSerializer.Serialize(new Api2Response { Amount = 5.6M }));
            var api1StreamResponse = TestHelper.GenerateStreamFromString(JsonSerializer.Serialize(new Api1Response { Total = 10.6M }));
            var xmlApiStreamResponse = TestHelper.GenerateStreamFromString("<xml><quote value='200'/></xml>");
            _apiCall.Setup(s => s.GetJsonResultAsync(It.Is<string>(g => g == @"https://api1Url"), It.IsAny<Json1ApiRequest>())).ThrowsAsync(new HttpRequestException());
            _apiCall.Setup(s => s.GetJsonResultAsync(It.Is<string>(g => g == @"https://api2Url"), It.IsAny<Json2ApiRequest>())).ReturnsAsync(api2StreamResponse);
            _apiCall.Setup(s => s.GetXMLResultAsync(It.Is<string>(g => g == @"https://xmlApiUrl"), It.IsAny<XmlApiRequest>())).ReturnsAsync(xmlApiStreamResponse);
            var dimensionsList = TestHelper.GetDimensionsList();
            var result = _controller.GetAsync(new AtHomeProject.Request.OfferRequest { DestinationAddress = "addressDestination", SourceAddress = "addressSource", Dimensions = dimensionsList });
            Assert.AreEqual(5.6, (await result).Value);
        }

        [Test]
        public async System.Threading.Tasks.Task Get_ExternalApiXML_ThrowExceptionTestAsync()
        {
            var api2StreamResponse = TestHelper.GenerateStreamFromString(JsonSerializer.Serialize(new Api2Response { Amount = 5.6M }));
            var api1StreamResponse = TestHelper.GenerateStreamFromString(JsonSerializer.Serialize(new Api1Response { Total = 10.6M }));
            var xmlApiStreamResponse = TestHelper.GenerateStreamFromString("<xml><quote value='200'/></xml>");
            _apiCall.Setup(s => s.GetJsonResultAsync(It.Is<string>(g => g == @"https://api1Url"), It.IsAny<Json1ApiRequest>())).ReturnsAsync(api1StreamResponse);
            _apiCall.Setup(s => s.GetJsonResultAsync(It.Is<string>(g => g == @"https://api2Url"), It.IsAny<Json2ApiRequest>())).ReturnsAsync(api2StreamResponse);
            _apiCall.Setup(s => s.GetXMLResultAsync(It.Is<string>(g => g == @"https://xmlApiUrl"), It.IsAny<XmlApiRequest>())).ThrowsAsync(new HttpRequestException());
            var dimensionsList = TestHelper.GetDimensionsList();
            var result = _controller.GetAsync(new AtHomeProject.Request.OfferRequest { DestinationAddress = "addressDestination", SourceAddress = "addressSource", Dimensions = dimensionsList });
            Assert.AreEqual(5.6, (await result).Value);
        }

        [Test]
        public async System.Threading.Tasks.Task Get_Two_ExternalApi_ThrowExceptionTestAsync()
        {
            var api2StreamResponse = TestHelper.GenerateStreamFromString(JsonSerializer.Serialize(new Api2Response { Amount = 5.6M }));
            var api1StreamResponse = TestHelper.GenerateStreamFromString(JsonSerializer.Serialize(new Api1Response { Total = 10.6M }));
            var xmlApiStreamResponse = TestHelper.GenerateStreamFromString("<xml><quote value='200'/></xml>");
            _apiCall.Setup(s => s.GetJsonResultAsync(It.Is<string>(g => g == @"https://api1Url"), It.IsAny<Json1ApiRequest>())).ReturnsAsync(api1StreamResponse);
            _apiCall.Setup(s => s.GetJsonResultAsync(It.Is<string>(g => g == @"https://api2Url"), It.IsAny<Json2ApiRequest>())).ThrowsAsync(new HttpRequestException());
            _apiCall.Setup(s => s.GetXMLResultAsync(It.Is<string>(g => g == @"https://xmlApiUrl"), It.IsAny<XmlApiRequest>())).ThrowsAsync(new HttpRequestException());
            var dimensionsList = TestHelper.GetDimensionsList();
            var result = _controller.GetAsync(new AtHomeProject.Request.OfferRequest { DestinationAddress = "addressDestination", SourceAddress = "addressSource", Dimensions = dimensionsList });
            Assert.AreEqual(10.6, (await result).Value);
        }

        [Test]
        public async System.Threading.Tasks.Task Get_All_ExternalApi_ThrowExceptionTestAsync()
        {
            var api2StreamResponse = TestHelper.GenerateStreamFromString(JsonSerializer.Serialize(new Api2Response { Amount = 5.6M }));
            var api1StreamResponse = TestHelper.GenerateStreamFromString(JsonSerializer.Serialize(new Api1Response { Total = 10.6M }));
            var xmlApiStreamResponse = TestHelper.GenerateStreamFromString("<xml><quote value='200'/></xml>");
            _apiCall.Setup(s => s.GetJsonResultAsync(It.Is<string>(g => g == @"https://api1Url"), It.IsAny<Json1ApiRequest>())).ThrowsAsync(new HttpRequestException());
            _apiCall.Setup(s => s.GetJsonResultAsync(It.Is<string>(g => g == @"https://api2Url"), It.IsAny<Json2ApiRequest>())).ThrowsAsync(new HttpRequestException());
            _apiCall.Setup(s => s.GetXMLResultAsync(It.Is<string>(g => g == @"https://xmlApiUrl"), It.IsAny<XmlApiRequest>())).ThrowsAsync(new HttpRequestException());
            var dimensionsList = TestHelper.GetDimensionsList();
            var result = _controller.GetAsync(new AtHomeProject.Request.OfferRequest { DestinationAddress = "addressDestination", SourceAddress = "addressSource", Dimensions = dimensionsList });
            Assert.IsTrue((await result).Result is NotFoundObjectResult);
        }
    }
}