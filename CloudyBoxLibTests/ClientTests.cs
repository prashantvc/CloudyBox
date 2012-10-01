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
        private readonly UserLogin _user = new UserLogin("zkxugmcicyf3i04", "e1ajz6c71paz28c");

        [TestMethod]
        public async Task When_Requested_For_The_Token_Then_Return_Token_And_Secret()
        {
            using (var client = new Client())
            {
                UserLogin login = await client.RequestToken();
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
        public async Task When_Requested_Get_The_Root_Folder_Metadata()
        {
            using (var client = new Client())
            {
                client.SetUserLoginToHandler(_user);
                var metadata = await client.GetRoot();

                Assert.IsNotNull(metadata);
                Assert.AreEqual(@"/", metadata.Path);
            }
        }

        [TestMethod]
        public async Task When_Requested_Get_The_Give_Folder_Metadata()
        {
            using (var client = new Client())
            {
                client.SetUserLoginToHandler(_user);
                var metadata = await client.GetMetadata("appfest");

                Assert.IsNotNull(metadata);
                Assert.AreEqual(@"/appfest", metadata.Path, true);
            }
        }

        [TestMethod]
        public async Task Given_User_Login_When_Account_Information_Requested_Then_Get_The_Infromation()
        {
            using (var client = new Client())
            {
                client.SetUserLoginToHandler(_user);
                var information = await client.GetAccountInformation();

                Assert.IsNotNull(information);
                Assert.AreEqual("pvc@outlook.com", information.Email);
            }
        }
    }
}
