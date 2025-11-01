using Microsoft.AspNetCore.Mvc;
using ChatSite.Models;
using ChatSite.Services;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ChatSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly FmpService _fmpService;

        public HomeController(FmpService fmpService)
        {
            _fmpService = fmpService;
        }

        public async Task<IActionResult> Index()
        {
            var stocks = await _fmpService.GetStockQuotesAsync();

            var products = stocks.Select(s => new Product
            {
                Id = s.Symbol.GetHashCode(),
                Name = s.Symbol,
                Price = s.Price,
                Sector = s.Sector,
                Volume = 0,
                PE = 0,
                ROE = 0,
                RSI = 0,
                MovingAverage = 0
            }).ToList();

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> FilterAll(decimal? minPrice, decimal? maxPrice, string[]? sector)
        {
            var stocks = await _fmpService.GetStockQuotesAsync();

            var products = stocks.Select(s => new Product
            {
                Id = s.Symbol.GetHashCode(),
                Name = s.Symbol,
                Price = s.Price,
                Sector = s.Sector,
                Volume = 0,
                PE = 0,
                ROE = 0,
                RSI = 0,
                MovingAverage = 0
            }).ToList();

            var filtered = products.AsEnumerable();

            if (minPrice.HasValue)
                filtered = filtered.Where(p => p.Price >= minPrice.Value);
            if (maxPrice.HasValue)
                filtered = filtered.Where(p => p.Price <= maxPrice.Value);
            if (sector != null && sector.Any())
                filtered = filtered.Where(p => sector.Contains(p.Sector));

            return View("Index", filtered.ToList());
        }
    }
}
