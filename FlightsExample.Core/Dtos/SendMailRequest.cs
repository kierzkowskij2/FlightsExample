namespace FlightsExample.Core.Dtos
{
    public class SendMailRequest
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string QRCode { get; set; } // TODO: it might be a good idea to send qr code
    }
}
