using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Project_QLNCC.Configuration;

namespace Project_QLNCC.Web.Startup
{
    [DependsOn(typeof(Project_QLNCCWebCoreModule))]
    public class Project_QLNCCWebMvcModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public Project_QLNCCWebMvcModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.Navigation.Providers.Add<Project_QLNCCNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(Project_QLNCCWebMvcModule).GetAssembly());
        }
    }
}
