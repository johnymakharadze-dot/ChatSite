using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ChatSite.Services
{
    public class FmpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string ApiKey = "demo"; // შეგიძლია შენი key ჩასვა როცა მოიპოვებ

        public FmpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<StockQuote>> GetStockQuotesAsync(string exchange = "NASDAQ")
        {
            var client = _httpClientFactory.CreateClient("fmp");
            var response = await client.GetAsync($"stock-screener?exchange={exchange}&limit=10&apikey={ApiKey}");
            
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            return JsonSerializer.Deserialize<List<StockQuote>>(json, options) ?? new List<StockQuote>();
        }
    }

    public class StockQuote
    {
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Sector { get; set; } = string.Empty;
    }
}
