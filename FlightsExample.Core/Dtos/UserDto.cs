namespace FlightsExample.Core.Dtos
{
    public class UserDto
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public AddressDto address { get; set; }
        public CompanyDto company { get; set; }
    }
}