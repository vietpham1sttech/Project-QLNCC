using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Project_QLNCC.Configuration.Dto;

namespace Project_QLNCC.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : Project_QLNCCAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
