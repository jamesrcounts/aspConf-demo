using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using ApprovalUtilities.Asp.Mvc;
using CarDealership.Models;

namespace CarDealership.Controllers
{
#if DEBUG

    public partial class HomeController
    {
        public ActionResult TestCars()
        {
            // get the data
            var theData = new[]{
                new Car{ Make="VW", Model="Passat", Year=2002},
                new Car{Make="Honda", Model="Fit", Year=2007}
            };

            // show the data
            return this.Cars(theData);
        }
    }

#endif

    public partial class HomeController : Controller
    {
        private CarLot repo;

        public HomeController()
            : this(new CarLot())
        {
        }

        public HomeController(CarLot repo)
        {
            this.repo = repo;
        }

        public ActionResult Index()
        {
            // Get the data
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            // shows the data
            return View();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private ActionResult Cars(IEnumerable<Car> theData)
        {
            return View( theData).Explicit():
        }

        public ActionResult Cars()
        {
            IEnumerable<Car> theData = this.repo.GetCars();

            return Cars(theData);
        }

        public ActionResult About()
        {
            // its data is null
            // show completely static data
            return View();
        }
    }
}