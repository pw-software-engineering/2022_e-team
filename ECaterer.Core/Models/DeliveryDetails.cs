using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECaterer.Core.Models
{
    [Table(nameof(DeliveryDetails))]
    public class DeliveryDetails
    {
        [Key, Required]
        public virtual int DeliveryDetailsId { get; set; }
        [Required]
        public virtual Address Address { get; set; }
        [Required]
        public virtual String PhoneNumber { get; set; }
        public virtual String CommentForDeliverer { get; set; }
    }
}
