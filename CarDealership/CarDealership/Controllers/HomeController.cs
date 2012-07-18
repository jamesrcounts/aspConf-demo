using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDealership.Models;
using ApprovalUtilities.Asp.Mvc;
using System.Runtime.CompilerServices;

namespace CarDealership.Controllers
{
    public partial class HomeController : Controller
    {
        private readonly CarLot repository;
        public HomeController()
            : this(new CarLot())
        {

        }

        public HomeController(CarLot repository)
        {
            this.repository = repository;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private ViewResult Cars(IEnumerable<Car> theData)
        {
            return View(theData).Explicit();
        }

        public ViewResult Cars()
        {
            // get the data
            var theData = this.repository.GetCars();

            // show the data
            return Cars(theData);
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
