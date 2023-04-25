namespace nbp_api.Models.Output
{
    public class MinMaxModel
    {
        public ValueModel Min { get; set; }
        public ValueModel Max { get; set; }

        public MinMaxModel(ValueModel min, ValueModel max)
        {
            Min = min;
            Max = max;
        }
    }
}
