using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApprovalTests.Asp;
using ApprovalTests.Asp.Mvc;
using ApprovalTests.Reporters;
using System.Web.Mvc;
using CarDealership.Controllers;

[assembly: UseReporter(typeof(FileLauncherReporter), typeof(TortoiseDiffReporter))]
namespace CarDealership.Tests.Views
{
    [TestClass]
    public class HomeViewsTest
    {
        [TestMethod]
        public void IndexViewTest()
        {
            PortFactory.MvcPort = 43453;
            MvcApprovals.VerifyMvcPage(new HomeController().Index);
        }

        [TestMethod]
        public void AboutViewTest()
        {

        }

        [TestMethod]
        public void CarsViewTest()
        {
            // will have some data
        }
    }
}
