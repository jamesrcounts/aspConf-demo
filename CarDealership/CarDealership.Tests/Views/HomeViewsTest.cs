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
            // make our target available on a webserver ctrl-f5
            // provide route to an action
            PortFactory.MvcPort = 2683;
            Func<ActionResult> actionDelegate = new HomeController().Index;
            // just metadata
            // homecontroller instance not used in test
            // delegate is never invoked

            // test it (RED)
            MvcApprovals.VerifyMvcPage(actionDelegate);

            // review
            // approve if appropriate
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
            // will have some data, thats interesting
        }
    }
}
