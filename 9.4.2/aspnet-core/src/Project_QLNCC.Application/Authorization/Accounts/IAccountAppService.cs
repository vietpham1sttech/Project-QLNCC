using System.Threading.Tasks;
using Abp.Application.Services;
using Project_QLNCC.Authorization.Accounts.Dto;

namespace Project_QLNCC.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
