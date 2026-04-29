using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Project_QLNCC.Authorization;

namespace Project_QLNCC
{
    [DependsOn(
        typeof(Project_QLNCCCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class Project_QLNCCApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<Project_QLNCCAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(Project_QLNCCApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
