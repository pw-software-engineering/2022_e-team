using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECaterer.Core.Models
{
    [Table(nameof(Address))]
    public class Address
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual string AddressId { get; set; }
        [Required]
        [StringLength(250)]
        public virtual string Street { get; set; }
        [Required]
        [StringLength(50)]
        public virtual string BuildingNumber { get; set; }
        [StringLength(50)]
        public virtual string ApartmentNumber { get; set; }
        [StringLength(10)]
        [Required]
        public virtual string PostCode { get; set; }
        [StringLength(50)]
        [Required]
        public virtual string City { get; set; }
    }
}
