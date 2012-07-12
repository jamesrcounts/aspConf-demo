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
            // Supply route to MVC action
            PortFactory.MvcPort = 2080;
            Func<ActionResult> actionDelegate = new HomeController().Index;  // This delegate is never invoked.  Its just a way to pass around metadata about the route

            // Test action Result (RED)
            MvcApprovals.VerifyMvcPage(actionDelegate);

            // Review failing result
            // Approve Result if appropriate
            // Test Again (GREEN)
            // Refactor
        }

        [TestMethod]
        public void TestAboutView()
        {
        }
    }
}