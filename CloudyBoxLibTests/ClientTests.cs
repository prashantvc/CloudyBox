using System.Diagnostics;
using System.Threading.Tasks;
using CloudyBoxLib;
using CloudyBoxLib.Model;
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
                UserLogin login = await client.GetToken();
                Assert.IsNotNull(login);
            }
        }

        [TestMethod]
        public void Given_A_Login_Details_And_CallBack_Url_Then_Create_Authorisation_Url()
        {
            using (var client = new Client())
            {
                var user = new UserLogin("n0vds74l8owc731", "h4b3usbjbzk8020");
                string url = client.CreateAuthoriseUrl(user, "http://www.cloudyboxapp.com");

                Debug.WriteLine(url);
                Assert.IsNotNull(url);
            }
        }

        [TestMethod]
        public async Task GetMetadata()
        {
            using (var client = new Client())
            {
                client.SetUserLoginToHandler(new UserLogin("zkxugmcicyf3i04", "e1ajz6c71paz28c"));
                var metadata = await client.GetMetadata();
                Assert.IsNotNull(metadata);
            }
        }
    }
}
