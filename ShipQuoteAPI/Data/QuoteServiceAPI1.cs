using ShipQuoteAPI.Models;
using Newtonsoft.Json;

namespace ShipQuoteAPI.Data
{
    public class QuoteServiceAPI1 : IQuoteService
    {
        public string ProviderName { get { return "API1"; } }

        public async Task<Quote> GetShipQuote(PackageQuoteQuery query)
        {
            Quote quote = new Quote() { Provider = this.ProviderName, Amount = -1 };
            try
            {
                using (var client = new HttpClient())
                {
                    string url = "http://google.com";
                    var body = this.GetRequestBody(query);
                   
                    //var result = await client.PostAsync(url, body); //actual request to external API
                    var result = await client.GetStringAsync(url);//calling google to mock request to external API
                   
                   
                    double amount = -1;
                    if (!(!String.IsNullOrEmpty(result) && double.TryParse((string)result, out amount))) //amount not valid
                    {                        
                        amount = 44;
                    }

                    quote = new Quote() { Provider = this.ProviderName, Amount = amount };
                }
            }
            catch (Exception ex)
            {
               //error handling logic
            }
            return quote;
        }

        private string GetRequestBody(PackageQuoteQuery query)
        {
            var body = new RequestBodyAPI1() { ContactAddress = query.SourceAddress, WarehouseAddress = query.SourceAddress, Package = query.Package };
            return JsonConvert.SerializeObject(body);
        }


        private class RequestBodyAPI1
        {
            public string ContactAddress { get; set; }
            public string WarehouseAddress { get; set; }
            public PackageModel Package { get; set; }
        }
    }
}
