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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual string ComplaintId { get; set; }
        [Required]
        public virtual string Description { get; set; }
        [Required]
        public virtual DateTime Date { get; set; }
        [Required]
        public virtual int Status { get; set; }
        public virtual string Answer { get; set; }
    }
}
