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

        [TestInitialize]
        public void Setup()
        {
            this.StartServer(MvcApplication.Directory,
                PortFactory.MvcPort,
                "/",
                "localhost");
        }

        [TestCleanup]
        public void Teardown()
        {
            this.StopServer();
        }
    }
}