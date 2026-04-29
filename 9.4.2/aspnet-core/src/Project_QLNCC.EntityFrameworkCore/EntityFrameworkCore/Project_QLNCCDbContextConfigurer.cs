using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Project_QLNCC.EntityFrameworkCore
{
    public static class Project_QLNCCDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<Project_QLNCCDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<Project_QLNCCDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
