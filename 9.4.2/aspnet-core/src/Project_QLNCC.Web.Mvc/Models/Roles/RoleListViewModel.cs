using System.Collections.Generic;
using Project_QLNCC.Roles.Dto;

namespace Project_QLNCC.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
