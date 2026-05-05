using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Project_QLNCC.Suppliers.Dto
{
    [AutoMapTo(typeof(Supplier))]
    public class CreateSupplierInput
    {
        [Required]
        [StringLength(20)]
        public string Code { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(20)]
        public string TaxCode { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(200)]
        public string Email { get; set; }

        public string Address { get; set; }

        [StringLength(100)]
        public string ContactPerson { get; set; }
    }
}
