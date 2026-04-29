using System.Collections.Generic;
using Project_QLNCC.Roles.Dto;

namespace Project_QLNCC.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
