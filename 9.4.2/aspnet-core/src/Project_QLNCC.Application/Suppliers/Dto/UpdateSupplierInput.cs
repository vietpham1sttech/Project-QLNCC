using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;
using Project_QLNCC.Suppliers;

namespace Project_QLNCC.Suppliers.Dto
{
    [AutoMapTo(typeof(Supplier))]
    public class UpdateSupplierInput : EntityDto<int>
    {
        [Required(ErrorMessage = "Mã nhà cung cấp là bắt buộc")]
        [StringLength(20, ErrorMessage = "Mã quá dài")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Tên nhà cung cấp là bắt buộc")]
        [StringLength(50, ErrorMessage = "Tên quá dài")]
        public string Name { get; set; }

        [StringLength(20)]
        public string TaxCode { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(200)]
        public string Email { get; set; }

        public string Address { get; set; }

        [StringLength(100)]
        public string ContactPerson { get; set; }

        public bool IsActive { get; set; }
    }
}