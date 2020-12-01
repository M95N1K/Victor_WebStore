using Microsoft.Extensions.Configuration;

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
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
        public T Get<T>(string url) => GetAsync<T>(url).Result;
        public async Task<T> GetAsync<T>(string url, CancellationToken Cancel = default)
        {
            var response = await _Client.GetAsync(url, Cancel);
            return await response.EnsureSuccessStatusCode().Content.ReadAsAsync<T>(Cancel);
        }
        #endregion

        #region Post
        public HttpResponseMessage Post<T>(string url, T item) => PostAsync<T>(url, item).Result;
        public async Task<HttpResponseMessage> PostAsync<T>(string url, T item, CancellationToken Cancel = default)
        {
            var response = await _Client.PostAsJsonAsync(url, item, Cancel);
            return response.EnsureSuccessStatusCode();
        }
        #endregion

        #region Put
        public HttpResponseMessage Put<T>(string url, T item) => PutAsync<T>(url, item).Result;
        public async Task<HttpResponseMessage> PutAsync<T>(string url, T item, CancellationToken Cancel = default)
        {
            var response = await _Client.PutAsJsonAsync(url, item,Cancel);
            return response.EnsureSuccessStatusCode();
        }
        #endregion

        #region Delete
        public HttpResponseMessage Delete(string url) => DeleteAsunc(url).Result;
        public async Task<HttpResponseMessage> DeleteAsunc(string url, CancellationToken Cancel = default)
        {
            var response = await _Client.DeleteAsync(url,Cancel);
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
