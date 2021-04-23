using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestTask2.MetaweatherAPI;

namespace TestTask2_Tests
{
    public class Tests
    {    
        private MetaweatherClient client;
        private Location location;

        [SetUp]
        public void Setup()
        {
            client = new MetaweatherClient();
            location = client.SearchLocation("min").Where(loc => loc.title == "Minsk").First();
        }

        [Test]
        public void CoordinateTest()
        {
            string latt_long = location.latt_long;
            latt_long = latt_long?.Replace(" ", "");
            int inputSeparator = latt_long.IndexOf(",");
            location.latt_long = latt_long?.Replace(".", ",");
            double latitude, longitude;

            if (!double.TryParse(location.latt_long.Remove(inputSeparator), out latitude))
                Assert.Fail();
            if (!double.TryParse(location.latt_long.Substring(inputSeparator + 1), out longitude))
                Assert.Fail();

            Assert.IsTrue((latitude < 54 && latitude > 53.8) && (longitude > 27.37 && longitude < 27.73));
        }

        [Test]
        public void TemperatureTest()
        {
            ConsolidatedWeather[] forecast = client.GetLocationInfoAndForecast(location.woeid).consolidated_weather;
            int[] temperatureInterval = new int[2];
            foreach (ConsolidatedWeather weather in forecast)
            {
                DateTime date = DateTime.Parse(weather.applicable_date);
                switch (date.Month)
                {
                    case 12: case 1: case 2:   //Winter
                        if (weather.the_temp > 0)
                            Assert.Fail("Winter temperature expected to be below zero");
                        break;
                    case 6: case 7: case 8:    //Summer
                        if (weather.the_temp <= 0)
                            Assert.Fail("Summer temperature expected to be above zero");
                        break;
                    case 9: case 10: case 11: case 3: case 4: case 5:  //Autumn or Spring
                        if (weather.the_temp <= -8 || weather.the_temp >= 30)
                            Assert.Fail("Too high or too low Autumn/Spring temperature");
                        break;
                    default:
                        break;
                }
            }
            Assert.Pass();
        }

        [Test]
        public void WeatherStateTest()
        {
            ConsolidatedWeather todayWeather = client.GetLocationInfoAndForecast(location.woeid).consolidated_weather.First();
            string state = todayWeather.weather_state_name;
            DateTime date = DateTime.Now;
            bool weatherStateExists = false;
            while (date > DateTime.Now.AddYears(-5))
            {
                date = date.AddDays(-1);
                IEnumerable<string> previousStates = client.GetForecast(834463, DateTime.Now.AddDays(-1)).Select(w => w.weather_state_name).Distinct();
                if (previousStates.Contains(state))
                {
                    weatherStateExists = true;
                    break;
                }
            }
            Assert.IsTrue(weatherStateExists);
        }
    }
}