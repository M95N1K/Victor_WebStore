using Microsoft.Extensions.Configuration;

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Victor_WebStore.Clients.Base
{
    public abstract class BaseClient : IDisposable
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

        #region Get
        public T Get<T>(string url) => GetAsunc<T>(url).Result;
        public async Task<T> GetAsunc<T>(string url)
        {
            var response = await _Client.GetAsync(url);
            return await response.EnsureSuccessStatusCode().Content.ReadAsAsync<T>();
        }
        #endregion

        #region Post
        public HttpResponseMessage Post<T>(string url, T item) => PostAsync<T>(url, item).Result;
        public async Task<HttpResponseMessage> PostAsync<T>(string url, T item)
        {
            var response = await _Client.PostAsJsonAsync(url, item);
            return response.EnsureSuccessStatusCode();
        }
        #endregion

        #region Put
        public HttpResponseMessage Put<T>(string url, T item) => PutAsync<T>(url, item).Result;
        public async Task<HttpResponseMessage> PutAsync<T>(string url, T item)
        {
            var response = await _Client.PutAsJsonAsync(url, item);
            return response.EnsureSuccessStatusCode();
        }
        #endregion

        #region Delete
        public HttpResponseMessage Delete(string url) => DeleteAsunc(url).Result;
        public async Task<HttpResponseMessage> DeleteAsunc(string url)
        {
            var response = await _Client.DeleteAsync(url);
            return response;
        } 
        #endregion

        public void Dispose()
        {
            
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
        }
    }
}
