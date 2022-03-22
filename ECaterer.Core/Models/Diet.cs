using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECaterer.Core.Models
{
    [Table(nameof(Diet))]
    public class Diet
    {
        [Key, Required]
        public virtual int DietId { get; set; }
        [Required]
        public virtual string Title { get; set; }
        [Required]
        public virtual string Description { get; set; }
        [Required]
        public virtual int Calories { get; set; }
        [Required]
        public virtual ICollection<Meal> Meals { get; set; }
        [Required]
        public virtual bool Vegan { get; set; }
    }
}
