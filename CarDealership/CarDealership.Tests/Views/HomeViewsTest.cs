using System;
using System.Web.Mvc;
using ApprovalTests.Asp.Mvc;
using ApprovalTests.Reporters;
using CarDealership.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarDealership.Tests.Views
{
    [TestClass]
    [UseReporter(typeof(TortoiseDiffReporter), typeof(FileLauncherReporter))]
    public class HomeViewsTest
    {
        [TestMethod]
        public void IndexViewTest()
        {
            // make the view available

            // provide route
            ApprovalTests.Asp.PortFactory.MvcPort = 50978;
            var controller = new HomeController();
            Func<ActionResult> method = controller.Index;

            // test it (red)
            MvcApprovals.VerifyMvcPage(method);

            // review it approve if appropriate
            // test it (green)
            // refactor.
        }

        [TestMethod]
        public void AboutViewTest()
        {
            // aboutViewTest
        }

        [TestMethod]
        public void CarsViewTest()
        {
            // some test with data, should be ineresting
        }
    }
}