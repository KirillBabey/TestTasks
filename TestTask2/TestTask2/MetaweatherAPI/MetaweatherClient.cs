using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace TestTask2.MetaweatherAPI
{
    public class MetaweatherClient
    {
        private HttpClient _client;

        public MetaweatherClient()
        {
            _client = new HttpClient();
        }

        /// <returns>All locations which name includes specified substring</returns>
        public Location[] SearchLocation(string searchSubstring)
        {
            string url = $"https://www.metaweather.com/api/location/search/?query={searchSubstring}";
            return Get<Location[]>(url);
        }

        /// <returns>Location information, and a 5 day forecast</returns>
        public LocationResult GetLocationInfoAndForecast(int locationWoeid)
        {
            string url = $"https://www.metaweather.com/api/location/{locationWoeid}/";
            return Get<LocationResult>(url);
        }

        /// <returns>Forecast array for specific location and date</returns>
        public ConsolidatedWeather[] GetForecast(int locationWoeid, DateTime forecastDate)
        {
            string url = $"https://www.metaweather.com/api/location/{locationWoeid}/{forecastDate.Year}/{forecastDate.Month}/{forecastDate.Day}/";
            return Get<ConsolidatedWeather[]>(url);
        }

        public TResponse Get<TResponse>(string requestUrl)
        {
            var response = _client.GetAsync(requestUrl).Result;
            Console.WriteLine(response.StatusCode);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = null;
                if (response.Content != null)
                {
                    responseContent = response.Content.ReadAsStringAsync().Result;
                }
                if (!string.IsNullOrWhiteSpace(responseContent))
                {
                    return JsonConvert.DeserializeObject<TResponse>(responseContent);
                }
                return default(TResponse);
            }
            else
            {
                throw new HttpRequestException();
            }
        }
    }
}