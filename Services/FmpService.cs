using System.Net.Http;
using System.Net.Http.Headers;
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
        // Free API Short Quote არ აბრუნებს CompanyName/Sector, ამიტომ ისინი შეიძლება დარჩეს ცარიელი
        public string CompanyName { get; set; } = "";
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
            _http.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<StockQuote>> GetStockQuotesAsync()
        {
            var symbols = new string[]
            {
                "AAPL", "MSFT", "GOOGL", "AMZN", "FB",
                "NVDA", "TSLA", "BABA", "JPM", "V"
            };

            var result = new List<StockQuote>();

            foreach (var symbol in symbols)
            {
                var url = $"https://financialmodelingprep.com/api/v3/quote-short/{symbol}?apikey={_apiKey}";

                try
                {
                    var data = await _http.GetFromJsonAsync<List<StockQuote>>(url);
                    if (data != null) result.AddRange(data);
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"HTTP Error for {symbol}: {ex.Message}");
                }
            }

            return result;
        }
    }
}
