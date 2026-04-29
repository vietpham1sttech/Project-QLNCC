using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Project_QLNCC.Authorization.Roles;
using Project_QLNCC.Authorization.Users;
using Project_QLNCC.MultiTenancy;
using Project_QLNCC.Suppliers;
namespace Project_QLNCC.EntityFrameworkCore
{
    public class Project_QLNCCDbContext : AbpZeroDbContext<Tenant, Role, User, Project_QLNCCDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public Project_QLNCCDbContext(DbContextOptions<Project_QLNCCDbContext> options)
            : base(options)
        {
        }
    }
}
