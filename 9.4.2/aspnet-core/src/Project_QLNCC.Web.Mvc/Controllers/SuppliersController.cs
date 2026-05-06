using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_QLNCC.Authorization;
using Project_QLNCC.Controllers;
using Project_QLNCC.Suppliers;
using System.Threading.Tasks;

namespace Project_QLNCC.Web.Controllers
{
    [AbpAuthorize(PermissionNames.Pages_Suppliers)]
    public class SuppliersController : Project_QLNCCControllerBase
    {
        private readonly ISupplierAppService _supplierAppService;

        public SuppliersController(ISupplierAppService supplierAppService)
        {
            _supplierAppService = supplierAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<PartialViewResult> EditModal(int id)
        {
            var supplier = await _supplierAppService.Get(new EntityDto<int>(id));
            return PartialView("_EditModal", supplier);
        }
    }
}
