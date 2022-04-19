using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ECaterer.Web.Infrastructure
{
    public class ApiClient: HttpClient
    {

        public ApiClient(IConfiguration configuration)
        {
            this.BaseAddress = new Uri(configuration["APIUrl"]);
            this.DefaultRequestHeaders.Accept.Clear();
            this.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
