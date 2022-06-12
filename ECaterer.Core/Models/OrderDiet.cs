using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECaterer.Core.Models
{
    public class OrderDiet
    {
        [Required]
        public virtual string OrderId { get; set; }
        public virtual Order Order { get; set; }
        [Required]
        public virtual string DietId { get; set; }
        public virtual Diet Diet { get; set; }
    }
}
