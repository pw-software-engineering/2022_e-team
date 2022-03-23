using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECaterer.Core.Models
{
    [Table(nameof(Ingredient))]
    public class Ingredient
    {
        [Key, Required]
        public virtual int IngredientId { get; set; }
        [Required]
        [StringLength(50)]
        public virtual string Name { get; set; }
    }
}
