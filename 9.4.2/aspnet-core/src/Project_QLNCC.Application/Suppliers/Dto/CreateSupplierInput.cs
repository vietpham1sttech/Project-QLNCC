using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Project_QLNCC.Suppliers.Dto
{
    [AutoMapTo(typeof(Supplier))]
    public class CreateSupplierInput
    {
        [Required(ErrorMessage = "Mã nhà cung cấp không được để trống")]
        [StringLength(20, ErrorMessage = "Mã nhà cung cấp không được vượt quá 20 ký tự")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Tên nhà cung cấp không được để trống")]
        [StringLength(50, ErrorMessage = "Tên nhà cung cấp không được vượt quá 50 ký tự")]
        public string Name { get; set; }

        [StringLength(20, ErrorMessage = "Mã số thuế không được vượt quá 20 ký tự")]
        public string TaxCode { get; set; }

        [StringLength(20, ErrorMessage = "Số điện thoại không được vượt quá 20 ký tự")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Địa chỉ Email không đúng định dạng")]
        [StringLength(200, ErrorMessage = "Email không được vượt quá 200 ký tự")]
        public string Email { get; set; }

        public string Address { get; set; }

        [StringLength(100, ErrorMessage = "Tên người liên hệ quá dài")]
        public string ContactPerson { get; set; }
    }
}
