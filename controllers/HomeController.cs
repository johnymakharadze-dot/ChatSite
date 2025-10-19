using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ChatSite.Models;

namespace ChatSite.Controllers
{
    public class HomeController : Controller
    {
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Apple", Price = 180, Sector = "Tech", Volume = 5000000, PE = 28, ROE = 35, RSI = 60, MovingAverage = 170 },
            new Product { Id = 2, Name = "Microsoft", Price = 320, Sector = "Tech", Volume = 3000000, PE = 32, ROE = 40, RSI = 55, MovingAverage = 310 },
            new Product { Id = 3, Name = "JPMorgan", Price = 150, Sector = "Finance", Volume = 4000000, PE = 12, ROE = 18, RSI = 45, MovingAverage = 152 },
            new Product { Id = 4, Name = "Pfizer", Price = 35, Sector = "Health", Volume = 2000000, PE = 9, ROE = 12, RSI = 30, MovingAverage = 40 },
            new Product { Id = 5, Name = "ExxonMobil", Price = 110, Sector = "Energy", Volume = 3500000, PE = 14, ROE = 25, RSI = 50, MovingAverage = 108 }
        };

        public IActionResult Index()
        {
            return View(products);
        }

        [HttpGet]
        public IActionResult FilterAll(decimal? minPrice, decimal? maxPrice, string[] sector,
            int? minVolume, int? maxVolume,
            bool lowPE = false, bool highROE = false,
            bool lowRSI = false, bool aboveMA = false)
        {
            var filtered = products.AsEnumerable();

            if (minPrice.HasValue) filtered = filtered.Where(p => p.Price >= minPrice.Value);
            if (maxPrice.HasValue) filtered = filtered.Where(p => p.Price <= maxPrice.Value);

            if (sector != null && sector.Length > 0)
                filtered = filtered.Where(p => sector.Contains(p.Sector));

            if (minVolume.HasValue) filtered = filtered.Where(p => p.Volume >= minVolume.Value);
            if (maxVolume.HasValue) filtered = filtered.Where(p => p.Volume <= maxVolume.Value);

            if (lowPE) filtered = filtered.Where(p => p.PE < 20);
            if (highROE) filtered = filtered.Where(p => p.ROE > 15);
            if (lowRSI) filtered = filtered.Where(p => p.RSI < 40);
            if (aboveMA) filtered = filtered.Where(p => p.Price > p.MovingAverage);

            return View("Index", filtered.ToList());
        }
    }
}
