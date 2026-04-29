using Abp.Application.Services.Dto;

namespace Project_QLNCC.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

