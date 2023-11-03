namespace FlightsExample.Core.Dtos
{
    //TODO: If I had more time I would introduce FE that sents these enum values (ex. dropdown tables key - string, value - enum)
    // so it would be easier to test
    public class RegisterRequestDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public Destination Source { get; set; }
        public Destination Destination { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Gender Gender { get; set; }
        public Meal Meal { get; set; }
        public FlightClass FlightClass { get; set; }
    }
}