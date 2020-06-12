using Newtonsoft.Json;
using ParkyWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ParkyWeb.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IHttpClientFactory _clientFactory;

        public Repository(IHttpClientFactory factory)
        {
            _clientFactory = factory;
        }
        public async Task<bool> CeateAsync(string url, T objectCreate)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (objectCreate != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(objectCreate),
                    Encoding.UTF8, "application/json");
            }
            else {
                return false;
            }
            var client = _clientFactory.CreateClient();
            HttpResponseMessage httpResponse = await client.SendAsync(request);
            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = _clientFactory.CreateClient();
            HttpResponseMessage httpResponse = await client.SendAsync(request);
            if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK) 
            {
                var jsonString = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);
            }
            return null;
        }

        public async Task<T> GetAsync(string url, int Id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = _clientFactory.CreateClient();

            HttpResponseMessage httpResponse = await client.SendAsync(request);
            if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            return null;
        }
    }
}
