namespace ChatSite.Models
{
    public class StockQuote
    {
        public string Symbol { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal ChangesPercentage { get; set; }
        public decimal Change { get; set; }
        public decimal DayLow { get; set; }
        public decimal DayHigh { get; set; }
        public long? Volume { get; set; }
    }
}
