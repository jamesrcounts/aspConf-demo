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
    public class HomeViewsTest : MvcViewTest
    {
        [TestMethod]
        public void IndexViewTest()
        {
            MvcApprovals.VerifyMvcPage(new HomeController().Index);
        }

        [TestMethod]
        public void AboutViewTest()
        {
            MvcApprovals.VerifyMvcPage(new HomeController().About);
        }
#if DEBUG
        [TestMethod]
        public void CarsViewTest()
        {
            MvcApprovals.VerifyMvcPage(new HomeController().TestCars);
        }
    }
#endif
}
