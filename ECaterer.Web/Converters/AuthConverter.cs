using ECaterer.Contracts;
using ECaterer.Web.DTO.ClientDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.Web.Converters
{
    public static class AuthConverter
    {
        public static AuthDTO ConvertBack(AuthenticatedUserModel input)
        {
            return new AuthDTO()
            {
                TokenJWT = input.Token
            };
        }
    }
}
