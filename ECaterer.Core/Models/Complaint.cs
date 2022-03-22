using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECaterer.Core.Models
{
    [Table(nameof(Complaint))]
    public class Complaint
    {
        [Key, Required]
        public virtual int ComplaintId { get; set; }
        [Required]
        public virtual string Description { get; set; }
        [Required]
        public virtual DateTime Date { get; set; }
        [Required]
        public virtual string Status { get; set; }
    }
}
