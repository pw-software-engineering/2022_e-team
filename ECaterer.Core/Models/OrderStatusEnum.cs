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
        public virtual int OrderStatusId { get; set; }
        [Required]
        public virtual string OrderStatusValue { get; set; }
    }
}
