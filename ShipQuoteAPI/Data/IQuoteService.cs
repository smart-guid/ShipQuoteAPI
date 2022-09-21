using ShipQuoteAPI.Models;

namespace ShipQuoteAPI.Data
{
    public interface IQuoteService
    {
        string ProviderName { get; }

        Task<Quote> GetShipQuote(PackageQuoteQuery query);
    }
}
