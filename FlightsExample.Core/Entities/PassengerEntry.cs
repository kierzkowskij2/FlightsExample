namespace FlightsExample.Core.Entities
{
    public class PassengerEntry
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public string PassengerCode { get; set; }
        public int DestinationCode { get; set; }
        public int SourceCode { get; set; }
        public string Gender { get; set; }
    }
}