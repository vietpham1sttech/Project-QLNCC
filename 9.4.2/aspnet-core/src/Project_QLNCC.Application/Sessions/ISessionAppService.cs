using System.Threading.Tasks;
using Abp.Application.Services;
using Project_QLNCC.Sessions.Dto;

namespace Project_QLNCC.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
