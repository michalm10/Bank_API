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

//{ "table":"C","currency":"frank szwajcarski","code":"CHF","rates":[{ "no":"076/C/NBP/2023","effectiveDate":"2023-04-19","bid":4.6525,"ask":4.7465},{ "no":"077/C/NBP/2023","effectiveDate":"2023-04-20","bid":4.6442,"ask":4.7380},{ "no":"078/C/NBP/2023","effectiveDate":"2023-04-21","bid":4.6520,"ask":4.7460},{ "no":"079/C/NBP/2023","effectiveDate":"2023-04-24","bid":4.6558,"ask":4.7498}]}