using CreditWorks.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CreditWorks.Controllers
{
    public class VehicleController : Controller
    {
        private DatabaseContext Context { get; }
        public VehicleController(DatabaseContext _context)
        {
            this.Context = _context;
        }
        public IActionResult Index(string lastDown, string sort = "Id")
        {
            List<Manufacturer> manufacturers = this.Context.Manufacturers.ToList();
            List<VehicleViewModel> vehicles = (from v in this.Context.Vehicles
                                               from w in this.Context.WeightCategories
                                               join m in this.Context.Manufacturers on v.ManufacturerId equals m.Id
                                               where w.StartWeight <= v.Weight && (w.EndWeight > v.Weight || w.EndWeight == null)
                                               select new VehicleViewModel { Vehicle = v, Manufacturer = m, WeightCategory = w, ManufacturerList = manufacturers }).ToList();
            switch (sort)
            {
                case "Id":
                    vehicles = lastDown != sort ? vehicles.OrderBy(v => v.Vehicle.Id).ToList() : vehicles.OrderByDescending(v => v.Vehicle.Id).ToList();
                    TempData["LastDownSort"] = lastDown != sort? sort : "";
                    break;
                case "OwnerName":
                    vehicles = lastDown != sort? vehicles.OrderBy(v => v.Vehicle.OwnerName).ToList() : vehicles.OrderByDescending(v => v.Vehicle.OwnerName).ToList();
                    TempData["LastDownSort"] = lastDown != sort? sort : "";
                    break;
                case "Weight":
                    vehicles = lastDown != sort? vehicles.OrderBy(v => v.Vehicle.Weight).ToList() : vehicles.OrderByDescending(v => v.Vehicle.Weight).ToList();
                    TempData["LastDownSort"] = lastDown != sort? sort : "";
                    break;
                case "ManufactureYear":
                    vehicles = lastDown != sort? vehicles.OrderBy(v => v.Vehicle.ManufactureYear).ToList() : vehicles.OrderByDescending(v => v.Vehicle.ManufactureYear).ToList();
                    TempData["LastDownSort"] = lastDown != sort? sort : "";
                    break;
                case "ManufacturerId":
                    vehicles = lastDown != sort? vehicles.OrderBy(v => v.Vehicle.ManufacturerId).ToList() : vehicles.OrderByDescending(v => v.Vehicle.ManufacturerId).ToList();
                    TempData["LastDownSort"] = lastDown != sort? sort : "";
                    break;
                case "WeightCategory":
                    vehicles = lastDown != sort? vehicles.OrderBy(v => v.WeightCategory.Id).ThenBy(v => v.Vehicle.Weight).ToList() : vehicles.OrderByDescending(v => v.WeightCategory.Id).ThenBy(v => v.Vehicle.Weight).ToList();
                    TempData["LastDownSort"] = lastDown != sort? sort : "";
                    break;
            }


            return View(vehicles);
        }

        [HttpPost]
        public ActionResult Create(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                if (vehicle.ManufactureYear < 0 || vehicle.Weight < 0)
                {
                    TempData["msg"] = "<script>alert('Vehicle not created. \\nPlease ensure that the vehicle has no negative numbers');</script>";
                    return RedirectToAction("Index"); 
                }
                this.Context.Vehicles.Add(vehicle);
                this.Context.SaveChanges();
                return RedirectToAction("Index");
            }
            TempData["msg"] = "<script>alert('Vehicle not created. \\nPlease ensure that all fields are filled out');</script>";
            return RedirectToAction("Index");
        }
    }
}
