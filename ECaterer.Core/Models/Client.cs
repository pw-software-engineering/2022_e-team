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
        public virtual string Name { get; set; }
        [Required]
        public virtual string LastName { get; set; }
        [Required]
        public virtual string Email { get; set; }
        [Required]
        public virtual Address Address { get; set; }
        [Required]
        public virtual string PhooneNumber { get; set; }

    }
}
