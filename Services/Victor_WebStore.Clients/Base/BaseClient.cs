using Microsoft.Extensions.Configuration;

using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Victor_WebStore.Clients.Base
{
    public abstract class BaseClient
    {
        protected readonly string _ServiceAddress;
        protected readonly HttpClient _Client;

        protected BaseClient(IConfiguration Configuration, string ServiceAdress)
        {
            _ServiceAddress = ServiceAdress;
            _Client = new HttpClient
            {
                BaseAddress = new Uri(Configuration["WebApiURL"]),
                DefaultRequestHeaders =
                {
                    Accept = {new MediaTypeWithQualityHeaderValue("application/json")}
                }
            };
        }
    }
}
