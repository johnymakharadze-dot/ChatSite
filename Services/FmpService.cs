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
        public decimal Price { get; set; }
        public string CompanyName { get; set; } = "N/A";
        public string Sector { get; set; } = "N/A";
    }

    public class FmpService
    {
        private readonly HttpClient _http;
        private readonly string _apiKey;

        public FmpService(IHttpClientFactory factory, IConfiguration configuration)
        {
            _http = factory.CreateClient("fmp");
            _apiKey = configuration["FinancialModelingPrep:ApiKey"] ?? "demo";

            _http.DefaultRequestHeaders.UserAgent.ParseAdd("ChatSiteApp/1.0");
        }

        public async Task<List<StockQuote>> GetStockQuotesAsync()
        {
            // უფასო API key–ით მხოლოდ ეს endpoint სტაბილურად მუშაობს
            var symbols = "AAPL,MSFT,GOOGL,AMZN,FB,NVDA,TSLA,BABA,JPM,V";
            var url = $"quote-short/{symbols}?apikey={_apiKey}";

            try
            {
                var data = await _http.GetFromJsonAsync<List<StockQuote>>(url);
                return data ?? new List<StockQuote>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Error: {ex.Message}");
                return new List<StockQuote>();
            }
        }
    }
}
