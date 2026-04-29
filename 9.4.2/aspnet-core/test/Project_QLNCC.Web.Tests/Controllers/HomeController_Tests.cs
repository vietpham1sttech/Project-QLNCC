using System.Threading.Tasks;
using Project_QLNCC.Models.TokenAuth;
using Project_QLNCC.Web.Controllers;
using Shouldly;
using Xunit;

namespace Project_QLNCC.Web.Tests.Controllers
{
    public class HomeController_Tests: Project_QLNCCWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}