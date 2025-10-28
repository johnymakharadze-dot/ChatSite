using System.Net.Http;
using System.Net.Http.Json;
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
        }

        // 10 NASDAQ Stocks
        public async Task<List<StockQuote>> GetStockQuotesAsync(string exchange = "NASDAQ")
        {
            // Free API: limit 10 + API Key
            var url = $"stock/list?exchange={exchange}&limit=10&apikey={_apiKey}";
            var data = await _http.GetFromJsonAsync<List<StockQuote>>(url);
            return data ?? new List<StockQuote>();
        }
    }
}
