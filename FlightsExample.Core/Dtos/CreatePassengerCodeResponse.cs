namespace FlightsExample.Core.Dtos
{
    public class CreatePassengerCodeResponse
    {
        public bool Success { get; set; }
        public string PassengerCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}