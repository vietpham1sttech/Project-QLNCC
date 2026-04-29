using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace Project_QLNCC.Web.Views
{
    public abstract class Project_QLNCCRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected Project_QLNCCRazorPage()
        {
            LocalizationSourceName = Project_QLNCCConsts.LocalizationSourceName;
        }
    }
}
