using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApprovalTests.Asp;

namespace CarDealership.Tests.Views
{
    public class MvcViewTest
    {
        public MvcViewTest()
        {
            PortFactory.MvcPort = 42110;
        }
    }
}
