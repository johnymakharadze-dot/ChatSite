namespace ChatSite.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Sector { get; set; } = string.Empty;
        public int Volume { get; set; }
        public decimal PE { get; set; }
        public decimal ROE { get; set; }
        public decimal RSI { get; set; }
        public decimal MovingAverage { get; set; }
    }
}
