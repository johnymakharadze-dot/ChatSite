using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace ChatSite.Services
{
    public class StockQuote
    {
        public string Symbol { get; set; } = "";
        public string CompanyName { get; set; } = "";
        public decimal Price { get; set; }
        public string Sector { get; set; } = "";
    }

    public class FmpService
    {
        private readonly HttpClient _http;
        private readonly string _apiKey;

        public FmpService(IHttpClientFactory factory, IConfiguration configuration)
        {
            _http = factory.CreateClient("fmp");
            _apiKey = configuration["FinancialModelingPrep:ApiKey"] ?? "";

            _http.DefaultRequestHeaders.UserAgent.ParseAdd("ChatSiteApp/1.0");
        }

        public async Task<List<StockQuote>> GetStockQuotesAsync()
        {
            // ყველა საჭირო NASDAQ სიმბოლო ერთ API request–ით
            var url = $"stock-screener?exchange=NASDAQ&limit=50&apikey={_apiKey}";

            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            return JsonSerializer.Deserialize<List<StockQuote>>(json, options) ?? new List<StockQuote>();
        }
    }
}
