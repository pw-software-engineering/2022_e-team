using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECaterer.Core.Models
{
    [Table(nameof(Allergent))]
    public class Allergent
    {
        [Key, Required]
        public virtual int AllergentId { get; set; }
        [StringLength(250)]
        [Required]
        public virtual string Name { get; set; }
        [ForeignKey("Meal")]
        public int MealId { get; set; }
        public Meal Meal { get; set; }
    }
}
