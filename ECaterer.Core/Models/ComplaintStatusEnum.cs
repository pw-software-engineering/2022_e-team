using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECaterer.Core.Models
{
    [Table(nameof(ComplaintStatusEnum))]
    public class ComplaintStatusEnum
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual string ComplaintStatusId { get; set; }
        [Required]
        [StringLength(50)]
        public virtual string ComplaintStatusValue { get; set; }
    }
}
