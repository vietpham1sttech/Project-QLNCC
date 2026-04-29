using Abp.AutoMapper;
using Project_QLNCC.Roles.Dto;
using Project_QLNCC.Web.Models.Common;

namespace Project_QLNCC.Web.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class EditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool HasPermission(FlatPermissionDto permission)
        {
            return GrantedPermissionNames.Contains(permission.Name);
        }
    }
}
