namespace FlightsExample.Core.Dtos
{
    public class RegisterResultDto
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public string QrCode { get; set; }
    }
}