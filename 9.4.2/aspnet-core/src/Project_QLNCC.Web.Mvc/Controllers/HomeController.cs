using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Project_QLNCC.Controllers;

namespace Project_QLNCC.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : Project_QLNCCControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
