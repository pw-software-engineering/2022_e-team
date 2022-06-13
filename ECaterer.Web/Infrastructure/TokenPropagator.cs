using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ECaterer.Web.Infrastructure
{
    public static class TokenPropagator
    {
        public static void Propagate(HttpRequest inRequest, HttpRequestMessage outRequest)
        {
            if (inRequest.Headers.TryGetValue("api-key", out var token))
            {
                outRequest.Headers.Add("api-key", token.ToString());
            }
        }
    
    }
}
