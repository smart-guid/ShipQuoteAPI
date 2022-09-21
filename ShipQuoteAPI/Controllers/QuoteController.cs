using Microsoft.AspNetCore.Mvc;
using ShipQuoteAPI.Data;
using ShipQuoteAPI.Models;
using System.Collections.Concurrent;

namespace ShipQuoteAPI.Controllers
{
    [Route("api/quotes")]
    public class QuoteController : Controller
    {
        private readonly List<IQuoteService> _quoteServices;

        public QuoteController()
        {
            _quoteServices = new List<IQuoteService>();
            _quoteServices.Add(new QuoteServiceAPI1());
            _quoteServices.Add(new QuoteServiceAPI2());
            _quoteServices.Add(new QuoteServiceAPI3());
        }

        [HttpGet] //[HttpPost]
        public async Task<IActionResult> GetQuote()//FromBody] PackageQuoteQuery query)
        {
            try
            {
                var query = new PackageQuoteQuery() { SourceAddress = "1", DestinationAddress = "2", Package = new PackageModel() { Depth = 1, Height = 1, Weight = 1, Width = 1 } };
              
                var tasks = _quoteServices.Select(x =>
                {
                    return x.GetShipQuote(query);
                });

                await Task.WhenAll(tasks.ToArray());

                List<Quote> quotes = new List<Quote>();
                foreach (var task in tasks)
                {
                    var result = ((Task<Quote>)task).Result;
                    quotes.Add(result);
                }

                return Ok(quotes.Min(x => x.Amount));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
