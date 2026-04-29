using Abp.Authorization;
using Project_QLNCC.Authorization.Roles;
using Project_QLNCC.Authorization.Users;

namespace Project_QLNCC.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
