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
        public static LoginUserModel ResolveClient()
        {
            return new LoginUserModel()
            {
                Email = "klient@klient.pl",
                Password = "Klient123!"
                //Email = ConfigurationManager.AppSettings["clientLogin"],
                //Password = ConfigurationManager.AppSettings["clientPassword"]
            };
        }

        

        public static LoginUserModel ResolveProducer()
        {
            return new LoginUserModel()
            {
                Email = "producent@producent.pl",
                Password = "Producent123!"
                //Email = ConfigurationManager.AppSettings["producerLogin"],
                //Password = ConfigurationManager.AppSettings["producerPassword"]
            };
        }


        public static LoginUserModel ResolveDeliverer()
        {
            return new LoginUserModel()
            {
                Email = "dostawca@dostawca.pl",
                Password = "Dostawca123!"
                //Email = ConfigurationManager.AppSettings["delivererLogin"],
                //Password = ConfigurationManager.AppSettings["delivererPassword"]
            };
        }
    }
}
