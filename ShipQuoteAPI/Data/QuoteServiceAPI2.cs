using ShipQuoteAPI.Models;
using Newtonsoft.Json;

namespace ShipQuoteAPI.Data
{
    public class QuoteServiceAPI2 : IQuoteService
    {
        public string ProviderName { get { return "API2"; } }

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
                        amount = 88;
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
            var body = new RequestBodyAPI2() { Consignee = query.SourceAddress, Consignor = query.DestinationAddress, Cartons = new List<PackageModel>() { query.Package } };
            return JsonConvert.SerializeObject(body);
        }


        private class RequestBodyAPI2
        {
            public string Consignee { get; set; }
            public string Consignor { get; set; }
            public List<PackageModel> Cartons { get; set; }
        }
    }
}
