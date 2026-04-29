using Abp.AspNetCore.Mvc.Authorization;
using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_QLNCC.Authorization;
using Project_QLNCC.Controllers;

namespace Project_QLNCC.Web.Controllers
{
    [AbpAuthorize(PermissionNames.Pages_Suppliers)]
    public class SuppliersController : Project_QLNCCControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
