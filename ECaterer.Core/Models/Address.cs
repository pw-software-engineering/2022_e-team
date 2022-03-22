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
        public virtual int AddressId { get; set; }
        [Required]
        public virtual string Street { get; set; }
        [Required]
        public virtual string BuildingNumber { get; set; }
        public virtual string ApartmentNumber { get; set; }
        [Required]
        public virtual string PostCode { get; set; }
        [Required]
        public virtual string City { get; set; }
    }
}
