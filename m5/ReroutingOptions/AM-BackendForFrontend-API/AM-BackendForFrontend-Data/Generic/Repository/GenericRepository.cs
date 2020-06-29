using AM_BackendForFrontend_Data.Data;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AM_BackendForFrontend_Data.Generic
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T: IEntity
    {
        private static readonly HttpClient _client = new HttpClient();

        public string ResourcePath { get; set; }
        public GenericRepository(IConfiguration configuration, IBaseAddress baseAddress)
        {
            _client.BaseAddress = baseAddress.BaseAddress;
            _client.Timeout = new TimeSpan(0, 0, 30);
            _client.DefaultRequestHeaders.Accept.Clear(); 
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            //Setup HttyRequestMessage
            var request = new HttpRequestMessage(HttpMethod.Get, $"{ResourcePath}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Get HttyResponseMessage
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode(); 
            var content = await response.Content.ReadAsStringAsync();
            var entities = JsonConvert.DeserializeObject<IEnumerable<T>>(content);

            return entities;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            //Setup HttyRequestMessage
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{ResourcePath}/{entity.Id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Get HttyResponseMessage
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode(); 
            var content = await response.Content.ReadAsStringAsync();

            return (content.Length==0);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            //Setup HttyRequestMessage
            var request = new HttpRequestMessage(HttpMethod.Get, $"{ResourcePath}/{id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Get HttyResponseMessage
            var response = await _client.SendAsync(request);
            //response.EnsureSuccessStatusCode(); 
            var content = await response.Content.ReadAsStringAsync();
            var entities = JsonConvert.DeserializeObject<T>(content);

            return entities;
        }

        public async Task<bool> StoreNewAsync(T entity)
        {
            //Setup HttyRequestMessage
            var entityToCreate = JsonConvert.SerializeObject(entity);
            var request = new HttpRequestMessage(HttpMethod.Post, $"{ResourcePath}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(entityToCreate);
            request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            //Get HttyResponseMessage
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode(); 
            var content = await response.Content.ReadAsStringAsync();
            var newEntity = JsonConvert.DeserializeObject<T>(content);

            return (newEntity.Id>0);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            //Setup HttyRequestMessage
            var entityToUpdate = JsonConvert.SerializeObject(entity);

            var request = new HttpRequestMessage(HttpMethod.Put, $"{ResourcePath}/{entity.Id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(entityToUpdate);
            request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            //Get HttyResponseMessage
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode(); 
            var content = await response.Content.ReadAsStringAsync();
            var newEntity = JsonConvert.DeserializeObject<T>(content);

            return (newEntity.Id > 0);
        }

    }
}
