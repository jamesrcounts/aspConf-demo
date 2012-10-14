using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using ApprovalUtilities.Asp.Mvc;
using CarDealership.Models;

namespace CarDealership.Controllers
{
#if DEBUG
    public partial class HomeController
    {
        // must be static
        public ActionResult TestCars()
        {
            // get some data - static
            var theData = new[]{
                new Car{Make="VW", Model="Passat", Year=2002},
                new Car{Make="Honda", Model="Fit", Year=2007}
            };

            // show some data
            return this.Cars(theData);
        }
    }
#endif

    public partial class HomeController : Controller
    {
        private CarLot repository;

        public HomeController()
            : this(new CarLot())
        {
        }

        public HomeController(CarLot cars)
        {
            this.repository = cars;
        }

        // static...
        public ActionResult About()
        {
            // there is no data... nothing is static
            return View(); // view is static
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private ActionResult Cars(IEnumerable<Car> theData)
        {
            // show some data - actually static
            return View(theData).Explicit();
        }

        // dynamic.. want it to be static during test.
        public ActionResult Cars()
        {
            // get some data - dynamic
            IEnumerable<Car> theData = this.repository.GetCars();

            return Cars(theData);
        }

        // still static...
        public ActionResult Index()
        {
            // Get some static data
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            // show the static data
            return View();
        }
    }
}