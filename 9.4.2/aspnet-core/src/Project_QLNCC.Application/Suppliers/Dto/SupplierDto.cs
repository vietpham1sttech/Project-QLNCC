using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;
using Project_QLNCC.Suppliers;
namespace Project_QLNCC.Suppliers.Dto
{
    [AutoMapFrom(typeof(Supplier))]
    public class SupplierDto : EntityDto<int>
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        public string TaxCode { get; set; }
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public bool IsActive { get; set; }
    }
}
