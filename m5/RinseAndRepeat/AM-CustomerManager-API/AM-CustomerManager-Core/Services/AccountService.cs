using AM_CustomerManager_Core.ContractModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AM_CustomerManager_Core.Services
{
    public class AccountService : IAccountService
    {
        private static readonly HttpClient _client = new HttpClient();
        public string ResourcePath { get; set; }

        public AccountService(IConfiguration configuration)
        {
            _client.BaseAddress = new Uri($"{configuration.GetValue<string>("BackForFrontEndAPI")}");
            _client.Timeout = new TimeSpan(0, 0, 30);
            _client.DefaultRequestHeaders.Accept.Clear();
        }

        public async Task<Account> GetAccountAsync(int id)
        {
            //Setup HttyRequestMessage
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/account/{id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Get HttyResponseMessage
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var entities = JsonConvert.DeserializeObject<Account>(content);

            return entities;
        }
    }
}
