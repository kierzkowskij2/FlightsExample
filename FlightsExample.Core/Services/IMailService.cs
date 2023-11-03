using FlightsExample.Core.Dtos;

namespace FlightsExample.Core.Services
{
    public interface IMailService
    {
        Task<bool> Send(SendMailRequest sendMailRequest);
    }
}