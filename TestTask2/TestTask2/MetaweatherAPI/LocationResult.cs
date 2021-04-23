namespace TestTask2.MetaweatherAPI
{
    public class LocationResult
    {
        public ConsolidatedWeather[] consolidated_weather { get; set; }
        public string time { get; set; }
        public string sun_rise { get; set; }
        public string sun_set { get; set; }
        public string timezone_name { get; set; }
        public Location parent { get; set; }
        public Source[] sources { get; set; }
        public string title { get; set; }
        public string location_type { get; set; }
        public int woeid { get; set; }
        public string latt_long { get; set; }
        public string timezone { get; set; }
    }
}