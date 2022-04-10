using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ECaterer.Web.Infrastructure
{
    public class AutheticatedApiClient : ApiClient
    {
        public AutheticatedApiClient(IConfiguration configuration, string tokenJWT):base(configuration) {
            this.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenJWT);
        }
    }
}
