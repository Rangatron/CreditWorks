namespace CreditWorks.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string OwnerName { get; set; }
        public decimal Weight { get; set; }
        public int ManufacturerId { get; set; }
        public int ManufactureYear { get; set; }
    }
}
