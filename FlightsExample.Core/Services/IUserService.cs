using FlightsExample.Core.Dtos;

namespace FlightsExample.Core.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>?> GetUsers(string searchTerm);
    }
}