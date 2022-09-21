namespace ShipQuoteAPI.Models
{
    public class PackageQuoteQuery
    {
        public string SourceAddress { get; set; }
        public string DestinationAddress { get; set; }
        public PackageModel Package { get; set; }    
    }
}
