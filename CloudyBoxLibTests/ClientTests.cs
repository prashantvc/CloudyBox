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
                UserLogin login = await client.CreateTokenRequest();
                
                Assert.IsNotNull(login);
            }
        }

        [TestMethod]
        public void Give_A_Login_Details_And_CallBack_Url_Then_Create_Authorisation_Url()
        {
            using (var client = new Client())
            {
                //http://www.cloudyboxapp.com/?uid=6147519&oauth_token=k0izzgsunryaczf
                var user = new UserLogin("vprwknzzk9engi9", "k0izzgsunryaczf");
                string url = client.CreateAuthoriseUrl(user, "http://www.cloudyboxapp.com");
                Debug.WriteLine(url);
                Assert.IsNotNull(url);
            }   
        }

        [TestMethod]
        public async Task AccessToken()
        {
            using (var client = new Client())
            {
                var user = new UserLogin("vprwknzzk9engi9", "k0izzgsunryaczf");
                client.SetUserLoginToHandler(user);
                string res = await client.AccessToken();
                Debug.WriteLine(res);
            }
        }
    }
}
