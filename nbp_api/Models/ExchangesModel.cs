namespace nbp_api.Models
{
    public class ExchangesAModel
    {
        public string table { get; set; }
        public string currency { get; set; }
        public string code { get; set; }
        public List<RatesModel> rates { get; set; }
    }

    public class ExchangesCModel
    {
        public string table { get; set; }
        public string currency { get; set; }
        public string code { get; set; }
        public List<BuyAskModel> rates { get; set; }
    }


    public class BuyAskModel
    {
        public string no { get; set; }
        public string effectiveDate { get; set; }
        public double bid { get; set; }
        public double ask { get; set; }
    }

    public class RatesModel
    {
        public string no { get; set; }
        public string effectiveDate { get; set; }
        public double mid { get; set; }
    }
}