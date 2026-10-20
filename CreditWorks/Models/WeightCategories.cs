namespace CreditWorks.Models
{
    public class WeightCategories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal StartWeight { get; set; }
        public decimal? EndWeight { get; set; }
        public string? Icon {  get; set; }
    }
}
