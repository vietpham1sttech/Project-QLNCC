using System.Collections.Generic;
using System.Linq;
using Project_QLNCC.Roles.Dto;
using Project_QLNCC.Users.Dto;

namespace Project_QLNCC.Web.Models.Users
{
    public class EditUserModalViewModel
    {
        public UserDto User { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }

        public bool UserIsInRole(RoleDto role)
        {
            return User.RoleNames != null && User.RoleNames.Any(r => r == role.NormalizedName);
        }
    }
}
