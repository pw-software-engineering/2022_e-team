using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECaterer.Core.Models
{
    [Table(nameof(Order))]
    public class Order
    {
        [Key, Required]
        public virtual int OrderId { get; set; }
        [Required]
        public virtual ICollection<Diet> Diets { get; set; }
        [Required]
        public virtual DeliveryDetails DeliveryDetails { get; set; }
        [Required]
        public virtual DateTime StartDate { get; set; }
        [Required]
        public virtual DateTime EndDate { get; set; }
        [Required]
        public virtual Decimal Price { get; set; }
        [Required]
        public virtual string  Status { get; set; }
        public virtual Complaint Complaint { get; set; }
    }
}
