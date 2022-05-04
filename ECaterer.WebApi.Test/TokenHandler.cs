using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Integration.Test
{
    public static class TokenHandler
    {
        private static string _token;

        public static void SetToken(string token)
        {
            _token = token;
        }

        public static string GetToken()
        {
            return _token;
        }
    }
}
