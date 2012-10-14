﻿using System;
using System.Web.Mvc;
using ApprovalTests.Asp;
using ApprovalTests.Asp.Mvc;
using ApprovalTests.Reporters;
using CarDealership.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarDealership.Tests.Views
{
    [TestClass]
    [UseReporter(typeof(TortoiseDiffReporter), typeof(FileLauncherWithDelayReporter))]
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
            // some test with data, should be ineresting
        }
    }
}