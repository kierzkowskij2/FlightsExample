using FlightsExample.Core.Dtos;
using FlightsExample.Core.Services;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace FlightsExample.Services.Services
{
    //TODO: I would configure it using different provider, but I managed to register in courier that sents messages "from my email"
    // I would also pass qrcode and figure out how to display it from courier email template
    public class MailService : IMailService
    {
        private readonly string _url;
        private readonly string _token;
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _url = _configuration["CourierUrl"] ?? string.Empty;
            _token = _configuration["CourierToken"] ?? string.Empty;
        }
        public async Task<bool> Send(SendMailRequest sendMailRequest)
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _token);
                string payload = "{ \"message\": { \"template\": \"8465G0SEKY4D2EH21JZ0SC2D546P\", \"to\": { \"email\":\"" + sendMailRequest.Email + "\" }, \"data\": { \"recipientName\": \"" + sendMailRequest.Name + "\"  }  }}";
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(new Uri(_url), content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}