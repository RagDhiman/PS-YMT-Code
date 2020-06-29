using AM_CustomerManager_Core.ContractModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AM_CustomerManager_Core.Services
{
    public class NotificationService : INotificationService
    {
        private static readonly HttpClient _client = new HttpClient();
        public string ResourcePath { get; set; }

        public NotificationService(IConfiguration configuration)
        {
            _client.BaseAddress = new Uri($"{configuration.GetValue<string>("AccountManagerAPI")}");
            _client.Timeout = new TimeSpan(0, 0, 30);
            _client.DefaultRequestHeaders.Accept.Clear();
        }

        public async Task<bool> PostToWebhookAsync(WebhookPost webpost)
        {
            //Setup HttyRequestMessage
            var entityToCreate = JsonConvert.SerializeObject(webpost);
            var request = new HttpRequestMessage(HttpMethod.Post, $"api/notification/PostToWebhook");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(entityToCreate);
            request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            //Get HttyResponseMessage
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var newEntity = JsonConvert.DeserializeObject<WebhookPost>(content);

            return (newEntity.Id > 0);
        }

        public async Task<bool> SendEmailAsync(Email email)
        {
            //Setup HttyRequestMessage
            var entityToCreate = JsonConvert.SerializeObject(email);
            var request = new HttpRequestMessage(HttpMethod.Post, $"api/notification/SendEmail");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(entityToCreate);
            request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            //Get HttyResponseMessage
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var newEntity = JsonConvert.DeserializeObject<Email>(content);

            return (newEntity.Id > 0);
        }

        public async Task<bool> SendSMSAsync(SMS sms)
        {
            //Setup HttyRequestMessage
            var entityToCreate = JsonConvert.SerializeObject(sms);
            var request = new HttpRequestMessage(HttpMethod.Post, $"api/notification/SendSMS");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(entityToCreate);
            request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            //Get HttyResponseMessage
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var newEntity = JsonConvert.DeserializeObject<SMS>(content);

            return (newEntity.Id > 0);
        }
    }
}
