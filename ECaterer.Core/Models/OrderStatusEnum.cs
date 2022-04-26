using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECaterer.Core.Models
{
    [Table(nameof(OrderStatusEnum))]
    public class OrderStatusEnum
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual string OrderStatusId { get; set; }
        [Required]
        [StringLength(50)]
        public virtual string OrderStatusValue { get; set; }
    }
}
