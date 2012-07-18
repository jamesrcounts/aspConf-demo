namespace CarDealership.Tests.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ApprovalTests.Asp;
    using CassiniDev;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MvcViewTest : CassiniDevServer
    {
        public MvcViewTest()
        {
            PortFactory.MvcPort = 24300;
            PortFactory.MvcPort++;
        }

        [TestInitialize]
        public void Setup()
        {
            this.StartServer(MvcApplication.Directory, PortFactory.MvcPort, "/", "localhost");
        }

        [TestCleanup]
        public void Teardown()
        {
            this.StopServer();
        }
    }
}