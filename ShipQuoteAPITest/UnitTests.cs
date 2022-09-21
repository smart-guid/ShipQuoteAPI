using Microsoft.AspNetCore.Mvc;
using ShipQuoteAPI.Controllers;
using ShipQuoteAPI.Data;
using ShipQuoteAPI.Models;

namespace TestQuoteAPI
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public async Task TestController()
        {
            QuoteController controller = new QuoteController();
            var result = await controller.GetQuote();        
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(22.0, ((OkObjectResult)result).Value);
        }

        [TestMethod]
        public async Task TestAPI1()
        {
            QuoteServiceAPI1 api = new QuoteServiceAPI1();
            var query = new PackageQuoteQuery() { SourceAddress = "1", DestinationAddress = "2", Package = new PackageModel() { Depth = 1, Height = 1, Weight = 1, Width = 1 } };
            Quote actualResult = await api.GetShipQuote(query);
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(44, actualResult.Amount);
        }

        [TestMethod]
        public async Task TestAP2()
        {
            QuoteServiceAPI2 api = new QuoteServiceAPI2();
            var query = new PackageQuoteQuery() { SourceAddress = "1", DestinationAddress = "2", Package = new PackageModel() { Depth = 1, Height = 1, Weight = 1, Width = 1 } };
            Quote actualResult = await api.GetShipQuote(query);
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(88, actualResult.Amount);
        }

        [TestMethod]
        public async Task TestAPI3()
        {
            QuoteServiceAPI3 api = new QuoteServiceAPI3();
            var query = new PackageQuoteQuery() { SourceAddress = "1", DestinationAddress = "2", Package = new PackageModel() { Depth = 1, Height = 1, Weight = 1, Width = 1 } };
            Quote actualResult = await api.GetShipQuote(query);
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(22, actualResult.Amount);
        }
    }
}