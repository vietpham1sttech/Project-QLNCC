using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using Project_QLNCC.Authorization.Users;
using Project_QLNCC.Editions;

namespace Project_QLNCC.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore)
        {
        }
    }
}
