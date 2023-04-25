namespace nbp_api.Models.Output
{
    public class ValueModel
    {
        public double Value { get; set; }
        public string Date { get; set; }

        public ValueModel(double value, string date)
        {
            Value = value;
            Date = date;
        }
    }
}
