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
    public class BankAccountRepositoryService : IBankAccountRepository
    {
        private static readonly HttpClient _client = new HttpClient();

        public BankAccountRepositoryService(IConfiguration configuration)
        {
            _client.BaseAddress = new Uri($"{configuration.GetValue<string>("CustomerManagerAPI")}");
            _client.Timeout = new TimeSpan(0, 0, 30);
            _client.DefaultRequestHeaders.Accept.Clear();
        }

        public async Task<bool> DeleteBankAccount(BankAccount BankAccount)
        {
            //Setup HttyRequestMessage
            var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/BankAccount/{BankAccount.Id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Get HttyResponseMessage
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            return (content.Length == 0);
        }

        public async Task<BankAccount[]> GetAllBankAccountsAsync(int CustomerId)
        {
            //Setup HttyRequestMessage
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/customer/{CustomerId}/BankAccount/");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Get HttyResponseMessage
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var entities = JsonConvert.DeserializeObject<BankAccount[]>(content);

            return entities;
        }

        public async Task<BankAccount> GetBankAccountAsync(int? CustomerId, int id)
        {
            //Setup HttyRequestMessage
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/customer/{CustomerId}/BankAccount/{id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Get HttyResponseMessage
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var entities = JsonConvert.DeserializeObject<BankAccount>(content);

            return entities;
        }

        public async Task<BankAccount> GetBankAccountAsync(int? CustomerId, int? id)
        {
            //Setup HttyRequestMessage
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/customer/{CustomerId}/BankAccount/{id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Get HttyResponseMessage
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var entities = JsonConvert.DeserializeObject<BankAccount>(content);

            return entities;
        }

        public async Task<bool> StoreNewBankAccountAsync(int CustomerID, BankAccount BankAccount)
        {
            //Setup HttyRequestMessage
            var entityToCreate = JsonConvert.SerializeObject(BankAccount);
            var request = new HttpRequestMessage(HttpMethod.Post, $"/api/customer/{CustomerID}/BankAccount/");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(entityToCreate);
            request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            //Get HttyResponseMessage
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var newEntity = JsonConvert.DeserializeObject<BankAccount>(content);

            return (newEntity.Id > 0);
        }

        public async Task<bool> UpdateBankAccountAsync(int CustomerId, BankAccount BankAccount)
        {
            //Setup HttyRequestMessage
            var entityToUpdate = JsonConvert.SerializeObject(BankAccount);

            var request = new HttpRequestMessage(HttpMethod.Put, $"/api/customer/{CustomerId}/BankAccount/{BankAccount.Id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(entityToUpdate);
            request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            //Get HttyResponseMessage
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var newEntity = JsonConvert.DeserializeObject<BankAccount>(content);

            return (newEntity.Id > 0);
        }
    }
}
