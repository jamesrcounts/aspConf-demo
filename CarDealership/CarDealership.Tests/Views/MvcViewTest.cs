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
            PortFactory.MvcPort = 2683;
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
            this.StartServer(MvcApplication.Directory, PortFactory.MvcPort, "/", "localhost");
        }
    }
}
