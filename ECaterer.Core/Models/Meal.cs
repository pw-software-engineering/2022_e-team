using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECaterer.Core.Models
{
    [Table(nameof(Meal))]
    public class Meal
    {
        [Key, Required]
        public virtual int MealId { get; set; }
        [Required]
        public virtual string Name { get; set; }
        [Required]
        public virtual string IngredientList { get; set; }
        [Required]
        public virtual string AllergentList { get; set; }
        [Required]
        public virtual int Calories { get; set; }
        [Required]
        public virtual bool Vegan { get; set; }
    }
}
