using Abp.Application.Services;
using Project_QLNCC.MultiTenancy.Dto;

namespace Project_QLNCC.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

