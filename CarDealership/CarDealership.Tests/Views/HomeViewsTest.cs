using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ApprovalTests.Asp;
using ApprovalTests.Asp.Mvc;
using ApprovalTests.Reporters;
using CarDealership.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[assembly: UseReporter(typeof(FileLauncherReporter), typeof(TortoiseDiffReporter))]

namespace CarDealership.Tests.Views
{
    [TestClass]
    public class HomeViewsTest : MvcViewTest
    {
        [TestMethod]
        public void TestIndexView()
        {
            MvcApprovals.VerifyMvcPage(new HomeController().Index);
        }

        [TestMethod]
        public void TestAboutView()
        {
            MvcApprovals.VerifyMvcPage(new HomeController().About);
        }

        [TestMethod]
        public void TestCarsView()
        {
        }
    }
}