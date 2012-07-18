#if DEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDealership.Models;

namespace CarDealership.Controllers
{
    public partial class HomeController
    {
        public ViewResult TestCars()
        {
            //Get the data
            var theData = new[]{
                new Car{Make="Vw", Model="Passat", Year=2002},
                new Car{Make="Honda", Model="Fit", Year=2007}
            };

            //show the data
            return Cars(theData);
        }
    }
}
#endif 