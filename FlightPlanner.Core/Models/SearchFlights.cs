namespace FlightPlanner.Core.Models
{
    public class SearchFlights : Entity
    {
        public string From { get; set; }
        public string To { get; set; }
        public string DepartureDate { get; set; }

        public SearchFlights(string from, string to, string departureDate)
        {
            From = from;
            To = to;
            DepartureDate = departureDate;
        }
    }
}
