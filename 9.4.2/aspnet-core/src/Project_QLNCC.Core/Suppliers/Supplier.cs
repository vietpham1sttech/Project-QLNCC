using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Project_QLNCC.Suppliers
{
    public class Supplier : Entity<int>
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(200)]
        public string Email { get; set; }

        [MaxLength]
        public string Address { get; set; }
    }
}
