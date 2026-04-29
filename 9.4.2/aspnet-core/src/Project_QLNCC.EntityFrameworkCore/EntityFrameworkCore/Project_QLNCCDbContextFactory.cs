using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Project_QLNCC.Configuration;
using Project_QLNCC.Web;

namespace Project_QLNCC.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class Project_QLNCCDbContextFactory : IDesignTimeDbContextFactory<Project_QLNCCDbContext>
    {
        public Project_QLNCCDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<Project_QLNCCDbContext>();
            
            /*
             You can provide an environmentName parameter to the AppConfigurations.Get method. 
             In this case, AppConfigurations will try to read appsettings.{environmentName}.json.
             Use Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") method or from string[] args to get environment if necessary.
             https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli#args
             */
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            Project_QLNCCDbContextConfigurer.Configure(builder, configuration.GetConnectionString(Project_QLNCCConsts.ConnectionStringName));

            return new Project_QLNCCDbContext(builder.Options);
        }
    }
}
