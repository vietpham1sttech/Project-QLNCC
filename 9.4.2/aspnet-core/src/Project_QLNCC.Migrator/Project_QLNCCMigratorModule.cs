using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Project_QLNCC.Configuration;
using Project_QLNCC.EntityFrameworkCore;
using Project_QLNCC.Migrator.DependencyInjection;

namespace Project_QLNCC.Migrator
{
    [DependsOn(typeof(Project_QLNCCEntityFrameworkModule))]
    public class Project_QLNCCMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public Project_QLNCCMigratorModule(Project_QLNCCEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(Project_QLNCCMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                Project_QLNCCConsts.ConnectionStringName
            );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(Project_QLNCCMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
