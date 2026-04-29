using Abp.AutoMapper;
using Project_QLNCC.Sessions.Dto;

namespace Project_QLNCC.Web.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}
