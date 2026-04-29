using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Project_QLNCC.Configuration;

namespace Project_QLNCC.Web.Host.Startup
{
    [DependsOn(
       typeof(Project_QLNCCWebCoreModule))]
    public class Project_QLNCCWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public Project_QLNCCWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(Project_QLNCCWebHostModule).GetAssembly());
        }
    }
}
