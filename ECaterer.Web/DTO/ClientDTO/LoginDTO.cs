using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.Web.DTO.ClientDTO
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
    }

    public enum UserType
    {
        Common = 1,
        Deliverer = 2,
        Producer = 3
    }
}
