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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual string DeliveryDetailsId { get; set; }
        [Required]
        public virtual Address Address { get; set; }
        [Required]
        [StringLength(20)]
        public virtual String PhoneNumber { get; set; }
        [StringLength(250)]
        public virtual String CommentForDeliverer { get; set; }
    }
}
