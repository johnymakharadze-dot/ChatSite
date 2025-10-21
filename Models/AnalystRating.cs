namespace ChatSite.Models
{
    public class AnalystRating
    {
        public int Id { get; set; }
        public string AnalystName { get; set; } = string.Empty;
        // რექომენდაციის სკალა: -5 (მაქსიმალურად უარყოფითი) ... 0 ... +5 (მაქსიმალურად დადებითი)
        public decimal Rating { get; set; } 
        public string Comment { get; set; } = string.Empty;
    }
}
