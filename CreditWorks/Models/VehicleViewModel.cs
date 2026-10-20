namespace CreditWorks.Models
{
    public class VehicleViewModel
    {
        public Vehicle Vehicle { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public WeightCategories WeightCategory { get; set; }
        public List<Manufacturer> ManufacturerList { get; set; }
    }
}
