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
    public class HomeViewsTest
    {
        [TestMethod]
        public void TestIndexView()
        {
            PortFactory.MvcPort = 24300;
            Func<ActionResult> actionDelegate = new HomeController().Index;
            MvcApprovals.VerifyMvcPage(actionDelegate);
        }

        [TestMethod]
        public void TestAboutView()
        {
        }

        [TestMethod]
        public void TestCarsView()
        {
        }
    }
}