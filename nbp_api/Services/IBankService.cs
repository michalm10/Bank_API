using nbp_api.Models.Output;

namespace nbp_api.Services
{
    public interface IBankService
    {
        public Task<string?> getRateOnDate(string code, DateOnly date);
        public Task<MinMaxModel?> getMinMax(string code, int quotations);
        public Task<ValueModel?> getDiff(string code, int quotations);

    }
}
