using CreditWorks.Controllers;
using CreditWorks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CreditWorks.Tests
{
    [TestClass]
    public class VehicleTests : Controller
    {
        private DatabaseContext Context { get; }
        public VehicleTests(DatabaseContext _context)
        {
            this.Context = _context;
        }

        [TestMethod]
        public void TestVehicleCategory ()
        {
            var controller = new VehicleController(Context);
            var result = controller.Index("") as ViewResult;
            var vehicle = (List<VehicleViewModel>) result.ViewData.Model;
            Assert.AreEqual(1, vehicle.FirstOrDefault().WeightCategory.Id);
            
        }
    }
}
