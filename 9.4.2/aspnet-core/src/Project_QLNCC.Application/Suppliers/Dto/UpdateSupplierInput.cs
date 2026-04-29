using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;
using Project_QLNCC.Suppliers;

namespace Project_QLNCC.Suppliers.Dto
{
    [AutoMapTo(typeof(Supplier))]
    public class UpdateSupplierInput : EntityDto<int>
    {
        [Required]
        public string Name { get; set; }

        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Address { get; set; }
    }
}