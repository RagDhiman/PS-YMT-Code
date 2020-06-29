using AccountsManager_Domain.DataAccess;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.RemoteDataRepositories
{
    public class CustomerRepositoryService : ICustomerRepository
    {
        private static readonly HttpClient _client = new HttpClient();

        public CustomerRepositoryService(IConfiguration configuration)
        {
            _client.BaseAddress = new Uri($"{configuration.GetValue<string>("CustomerManagerAPI")}");
            _client.Timeout = new TimeSpan(0, 0, 30);
            _client.DefaultRequestHeaders.Accept.Clear();
        }

        public async Task<bool> DeleteCustomer(Customer Customer)
        {
            //Setup HttyRequestMessage
            var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/customer/{Customer.Id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Get HttyResponseMessage
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            return (content.Length == 0);
        }

        public async Task<Customer[]> GetAllCustomersAsync()
        {
            //Setup HttyRequestMessage
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/customer/");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Get HttyResponseMessage
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var entities = JsonConvert.DeserializeObject<Customer[]>(content);

            return entities;
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            //Setup HttyRequestMessage
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/customer/{id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Get HttyResponseMessage
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var entities = JsonConvert.DeserializeObject<Customer>(content);

            return entities;
        }

        public async Task<bool> StoreNewCustomerAsync(Customer Customer)
        {
            //Setup HttyRequestMessage
            var entityToCreate = JsonConvert.SerializeObject(Customer);
            var request = new HttpRequestMessage(HttpMethod.Post, $"/api/customer/");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(entityToCreate);
            request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            //Get HttyResponseMessage
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var newEntity = JsonConvert.DeserializeObject<Customer>(content);

            return (newEntity.Id > 0);
        }

        public async Task<bool> UpdateCustomerAsync(Customer Customer)
        {
            //Setup HttyRequestMessage
            var entityToUpdate = JsonConvert.SerializeObject(Customer);

            var request = new HttpRequestMessage(HttpMethod.Put, $"/api/customer/{Customer.Id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(entityToUpdate);
            request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            //Get HttyResponseMessage
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var newEntity = JsonConvert.DeserializeObject<Customer>(content);

            return (newEntity.Id > 0);
        }
    }
}
