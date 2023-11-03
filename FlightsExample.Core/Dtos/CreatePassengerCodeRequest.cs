namespace FlightsExample.Core.Dtos
{
    public class CreatePassengerCodeRequest
    {
        public Destination Source { get; set; }
        public Destination Destination { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Gender Gender { get; set; }
        public Meal Meal { get; set; }
        public FlightClass FlightClass { get; set; }
        public int Age { get; set; }
    }
}