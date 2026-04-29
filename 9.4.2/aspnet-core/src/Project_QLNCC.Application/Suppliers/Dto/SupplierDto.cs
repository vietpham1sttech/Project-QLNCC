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
        [StringLength(50)]
        public string Name { get; set; }
        
        public string Phone { get; set; }

        [EmailAddress]
        [Required]
        [StringLength(200)]
        public string Email { get; set; }
       
        public string Address { get; set; }
    }
}
