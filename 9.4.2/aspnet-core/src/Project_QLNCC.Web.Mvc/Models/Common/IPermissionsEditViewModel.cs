using System.Collections.Generic;
using Project_QLNCC.Roles.Dto;

namespace Project_QLNCC.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}