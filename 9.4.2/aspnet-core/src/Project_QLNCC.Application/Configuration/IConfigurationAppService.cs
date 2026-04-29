using System.Threading.Tasks;
using Project_QLNCC.Configuration.Dto;

namespace Project_QLNCC.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
