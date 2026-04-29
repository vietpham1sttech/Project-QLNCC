using Abp.MultiTenancy;
using Project_QLNCC.Authorization.Users;

namespace Project_QLNCC.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
