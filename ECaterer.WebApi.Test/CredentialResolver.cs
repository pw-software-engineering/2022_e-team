using ECaterer.Contracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Integration.Test
{
    public static class CredentialResolver
    {
        public static LoginUserModel ResolveProducer()
        {
            return new LoginUserModel()
            {
                Email = "prodcer@login.com",
                Password = "1234"
            };
        }

        public static LoginUserModel ResolveDeliverer()
        {
            return new LoginUserModel()
            {
                Email = ConfigurationManager.AppSettings["delivererLogin"],
                Password = ConfigurationManager.AppSettings["delivererPassword"]
            };
        }
    }
}
