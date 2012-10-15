using System;
using System.Web.Mvc;
using ApprovalTests.Asp;
using ApprovalTests.Asp.Mvc;
using ApprovalTests.Reporters;
using CarDealership.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarDealership.Tests.Views
{
    [TestClass]
    [UseReporter(typeof(DiffReporter),
        typeof(FileLauncherWithDelayReporter))]
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

        [TestMethod]
        public void CarsViewTest()
        {
            MvcApprovals.VerifyMvcPage(new HomeController().TestCars);
        }
    }
}