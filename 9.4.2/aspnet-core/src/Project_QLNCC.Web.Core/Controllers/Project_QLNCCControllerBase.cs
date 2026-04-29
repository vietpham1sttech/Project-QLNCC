using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Project_QLNCC.Controllers
{
    public abstract class Project_QLNCCControllerBase: AbpController
    {
        protected Project_QLNCCControllerBase()
        {
            LocalizationSourceName = Project_QLNCCConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
