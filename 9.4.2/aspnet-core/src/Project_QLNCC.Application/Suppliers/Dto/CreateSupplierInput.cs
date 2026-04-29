using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Project_QLNCC.Suppliers.Dto
{
    [AutoMapTo(typeof(Supplier))]
    public class CreateSupplierInput
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
