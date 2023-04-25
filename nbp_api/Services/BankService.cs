using Microsoft.Extensions.Options;
using nbp_api.Config;
using nbp_api.Models;
using nbp_api.Models.Output;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Net.WebRequestMethods;

namespace nbp_api.Services
{
    public class BankService : IBankService
    {
        private readonly string formatJson = "?format=json";
        private readonly BankAPI _apiConfig;
        private readonly HttpClient _client;

        public BankService(HttpClient client, IOptions<BankAPI> apiConfig) 
        {
            _client = client;
            _apiConfig = apiConfig.Value;
        }

        public async Task<string?> getRateOnDate(string code, DateOnly date)
        {
            var dateFormatted = date.ToString("yyyy-MM-dd");
            var response = await _client.GetAsync($"{_apiConfig.URL}a/{code}/{dateFormatted}{formatJson}");
            try
            {
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadFromJsonAsync<ExchangesAModel>();
                if (responseBody is null) throw new NullReferenceException(nameof(responseBody));

                double rate = responseBody.rates[0].mid;
                return rate.ToString();
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task<MinMaxModel?> getMinMax(string code, int quotations)
        {
            var response = await _client.GetAsync($"{_apiConfig.URL}a/{code}/last/{quotations}{formatJson}");
            try
            {
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadFromJsonAsync<ExchangesAModel>();
                if (responseBody is null) throw new NullReferenceException(nameof(responseBody));

                var min = responseBody.rates.MinBy(item => item.mid);
                var max = responseBody.rates.MaxBy(item => item.mid);
                return new MinMaxModel(new ValueModel(min.mid, min.effectiveDate), new ValueModel(max.mid, max.effectiveDate));
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task<ValueModel?> getDiff(string code, int quotations)
        {
            var response = await _client.GetAsync($"{_apiConfig.URL}c/{code}/last/{quotations}{formatJson}");
            try
            {
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadFromJsonAsync<ExchangesCModel>();
                if (responseBody is null) throw new NullReferenceException(nameof(responseBody));

                var diff = responseBody.rates.MaxBy(item => Math.Abs(item.bid - item.ask));
                return new ValueModel(Math.Abs(diff.bid - diff.ask), diff.effectiveDate);
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
    }
}
