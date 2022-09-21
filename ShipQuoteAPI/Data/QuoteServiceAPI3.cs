using ShipQuoteAPI.Models;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace ShipQuoteAPI.Data
{
    public class QuoteServiceAPI3 : IQuoteService
    {
        public string ProviderName { get { return "API3"; } }

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

                    double amount = 22;// this.ParseQuoteAmount(result);

                    quote = new Quote() { Provider = this.ProviderName, Amount = amount };
                }
            }
            catch (Exception ex)
            {
                //error handling logic
            }
            return quote;
        }

        private double ParseQuoteAmount(string xmlOutput)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(RequestResponseAPI3));
            using (StringReader reader = new StringReader(xmlOutput))
            {
                RequestResponseAPI3 result = (RequestResponseAPI3)serializer.Deserialize(reader);
                return result.Quote;
            }
        }

        private string GetRequestBody(PackageQuoteQuery query)
        {
            var body = new RequestBodyAPI3() { Source = query.SourceAddress, Destination = query.DestinationAddress, Packages = new List<PackageModel>() { query.Package } };
            using (var stringwriter = new System.IO.StringWriter())
            {
                var serializer = new XmlSerializer(typeof(RequestBodyAPI3));
                serializer.Serialize(stringwriter, body);
                return stringwriter.ToString();
            }
        }

        public class RequestBodyAPI3
        {
            public string Source { get; set; }
            public string Destination { get; set; }
            public List<PackageModel> Packages { get; set; }
        }

        private class RequestResponseAPI3
        {
            public double Quote { get; set; }
        }
    }
}
