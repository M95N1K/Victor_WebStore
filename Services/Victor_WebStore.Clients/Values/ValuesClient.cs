using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using Victor_WebStore.Clients.Base;
using Victor_WebStore.Interfaces.TestApi;
using System.Net.Http;
using System.Linq;

namespace Victor_WebStore.Clients.Values
{
    public class ValuesClient : BaseClient, IValueService
    {

        public ValuesClient(IConfiguration Configuration) : base(Configuration, "api/values") { }


        public IEnumerable<string> Get()
        {
            var respone = _Client.GetAsync(_ServiceAddress).Result;
            if (respone.IsSuccessStatusCode)
                return respone.Content.ReadAsAsync<IEnumerable<string>>().Result;

            return Enumerable.Empty<string>();
        }

        public string Get(int id)
        {
            var respone = _Client.GetAsync($"{_ServiceAddress}/{id}").Result;
            if (respone.IsSuccessStatusCode)
                return respone.Content.ReadAsAsync<string>().Result;

            return string.Empty;
        }

        public Uri Post(string value)
        {
            var respone = _Client.PostAsJsonAsync(_ServiceAddress, value).Result;
            return respone.EnsureSuccessStatusCode().Headers.Location;
        }

        public HttpStatusCode Update(int id, string value)
        {
            var respone = _Client.PutAsJsonAsync($"{_ServiceAddress}/{id}", value).Result;
            return respone.EnsureSuccessStatusCode().StatusCode;
        }

        public HttpStatusCode Delete(int id)
        {
            var respone = _Client.DeleteAsync($"{_ServiceAddress}/{id}").Result;
            return respone.StatusCode;
        }
    }
}
