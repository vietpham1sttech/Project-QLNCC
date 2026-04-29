using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Project_QLNCC.EntityFrameworkCore;
using Project_QLNCC.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Project_QLNCC.Web.Tests
{
    [DependsOn(
        typeof(Project_QLNCCWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class Project_QLNCCWebTestModule : AbpModule
    {
        public Project_QLNCCWebTestModule(Project_QLNCCEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(Project_QLNCCWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(Project_QLNCCWebMvcModule).Assembly);
        }
    }
}