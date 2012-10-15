using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApprovalTests.Asp;
using CassiniDev;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarDealership.Tests.Views
{
    [TestClass]
    public class MvcViewTest : CassiniDevServer
    {
        public MvcViewTest()
        {
            PortFactory.MvcPort = 50978;
            PortFactory.MvcPort++;
        }

        [TestCleanup]
        public void Teardown()
        {
            this.StopServer();
        }

        [TestInitialize]
        public void Setup()
        {
            this.StartServer(@"../../../CarDealership", PortFactory.MvcPort, "/", "localhost");
        }
    }
}