using Abp.AspNetCore.Mvc.ViewComponents;

namespace Project_QLNCC.Web.Views
{
    public abstract class Project_QLNCCViewComponent : AbpViewComponent
    {
        protected Project_QLNCCViewComponent()
        {
            LocalizationSourceName = Project_QLNCCConsts.LocalizationSourceName;
        }
    }
}
