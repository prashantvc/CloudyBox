using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudyBoxLib;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace CloudyBoxLibTests
{
    [TestClass]
    public class ClientTests
    {
        [TestMethod]
        public async Task When_Requested_For_The_Token_Then_Return_Token_And_Secret()
        {
            using (var client = new Client())
            {
                string token = await client.CreateTokenRequest();
                Debug.WriteLine(token);
                Assert.IsTrue(!string.IsNullOrEmpty(token));
            }
        }
    }
}
