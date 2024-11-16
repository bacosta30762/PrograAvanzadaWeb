namespace ProyectoPrograAvanzadaWeb.Models
{
    public class FlightResponse
    {
        public Pagination Pagination { get; set; }
        public List<Vuelo> Data { get; set; }
    }

    public class Pagination
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }
    }

    public class Vuelo
    {
        public string FlightDate { get; set; }
        public string FlightStatus { get; set; }
        public Departure Departure { get; set; }
        public Arrival Arrival { get; set; }
        public Airline Airline { get; set; }
        public Flight Flight { get; set; }
    }

    public class Departure
    {
        public string Airport { get; set; }
        public string Timezone { get; set; }
        public string Iata { get; set; }
        public string Icao { get; set; }
        public string Terminal { get; set; } 
        public string Gate { get; set; }     
        public string Scheduled { get; set; }
        public string Estimated { get; set; }
    }

    public class Arrival
    {
        public string Airport { get; set; }
        public string Timezone { get; set; }
        public string Iata { get; set; }
        public string Icao { get; set; }
        public string Terminal { get; set; } 
        public string Gate { get; set; }     
        public string Scheduled { get; set; }
        public string Estimated { get; set; }
    }

    public class Airline
    {
        public string Name { get; set; }
        public string Iata { get; set; }
    }


    public class Flight
    {
        public string Number { get; set; }
    }

}
