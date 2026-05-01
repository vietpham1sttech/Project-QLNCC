using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;
using Project_QLNCC.Suppliers;
namespace Project_QLNCC.Suppliers.Dto
{
    [AutoMapFrom(typeof(Supplier))]
    public class SupplierDto : EntityDto<int>
    {
        [Required(ErrorMessage = "Thiếu mã nhà cung cấp")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Thiếu tên nhà cung cấp")]
        public string Name { get; set; }

        public string TaxCode { get; set; }
        public string Phone { get; set; }

        [Required(ErrorMessage = "Thiếu Email nhà cung cấp")]
        [EmailAddress(ErrorMessage = "Email sai định dạng")]
        public string Email { get; set; }

        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public bool IsActive { get; set; }
    }
}
