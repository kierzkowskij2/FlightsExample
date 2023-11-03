using FlightsExample.Core.Dtos;
using FlightsExample.Core.Services;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace FlightsExample.Services.Services
{
    public class UserService : IUserService
    {
        private readonly string _url;
        private readonly IConfiguration _configuration;
        public UserService(IConfiguration configuration) 
        {
            _configuration = configuration;
            _url = _configuration["UsersUrl"] ?? string.Empty;
        }

        public async Task<IEnumerable<UserDto>?> GetUsers(string searchTerm)
        {
            try
            {
                using var client = new HttpClient();
                var response = await client.GetAsync(_url);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var users = JsonSerializer.Deserialize<List<UserDto>>(responseContent);
                    return users?.Where(x => x.username.Contains(searchTerm)).ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}