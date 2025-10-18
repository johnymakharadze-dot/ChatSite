using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ChatSite.Controllers
{
    public class HomeController : Controller
    {
        // სტატიკური სია პროდუქტების
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Item 1", Price = 50 },
            new Product { Id = 2, Name = "Item 2", Price = 150 },
            new Product { Id = 3, Name = "Item 3", Price = 300 }
        };

        public IActionResult Index()
        {
            return View(products);
        }

        [HttpGet]
        public IActionResult FilterPrices(decimal? minPrice, decimal? maxPrice)
        {
            var filtered = products.AsEnumerable();

            if (minPrice.HasValue)
                filtered = filtered.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                filtered = filtered.Where(p => p.Price <= maxPrice.Value);

            return View("Index", filtered.ToList());
        }
    }

    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

       // public string Name { get; set; }
        public decimal Price { get; set; }

        
    }

    
}
