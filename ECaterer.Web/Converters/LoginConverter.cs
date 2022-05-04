using ECaterer.Contracts;
using ECaterer.Web.DTO.ClientDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.Web.Converters
{
    public static class LoginConverter
    {
        public static LoginUserModel Convert(LoginDTO input)
        {
            return new LoginUserModel() { 
                Email = input.Email,
                Password = input.Password
            };
        }
    }
}
