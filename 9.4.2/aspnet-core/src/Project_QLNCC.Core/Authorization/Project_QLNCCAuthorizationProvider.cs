using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Project_QLNCC.Authorization
{
    public class Project_QLNCCAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            context.CreatePermission(PermissionNames.Pages_Suppliers, L("Suppliers"));

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, Project_QLNCCConsts.LocalizationSourceName);
        }
    }
}
