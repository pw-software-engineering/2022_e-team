using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECaterer.Core.Models
{
    [Table(nameof(Client))]
    public class Client
    {
        [Key, Required]
        public virtual int ClientId { get; set; }
        [Required]
        [StringLength(50)]
        public virtual string Name { get; set; }
        [Required]
        [StringLength(50)]
        public virtual string LastName { get; set; }
        [Required]
        [StringLength(250)]
        public virtual string Email { get; set; }
        [Required]
        public virtual Address Address { get; set; }
        [Required]
        [StringLength(20)]
        public virtual string PhoneNumber { get; set; }

    }
}
