namespace FlightsExample.Core.Services
{
    public interface IQRCodeService
    {
        string Create(string passengerCode);
    }
}