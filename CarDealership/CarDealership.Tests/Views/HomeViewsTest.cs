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
            // make our views available? must be on a websever CTRL-f5
            // give route to action which produces the view
            PortFactory.MvcPort = 42110;
            Func<ActionResult> actionDelegate = new HomeController().Index;
            // home controller never used
            // delegate is never invoked.
            // just metadata

            // run the test (RED)
            MvcApprovals.VerifyMvcPage(actionDelegate);

            // approve results
            // run again (GREEN)
            // refactor
        }

        [TestMethod]
        public void AboutViewTest()
        {

        }

        [TestMethod]
        public void CarsViewTest()
        {
            // this will have some data
        }
    }
}
