﻿namespace ServerManagement.Models
{
    public static class CityRepository
    {
        private static List<string> cities = new List<string>
        {
            "Toronto",
            "Montreal",
            "Ottawa",
            "Calgary",
            "Halifax"
        };

        public static List<string> GetCities() => cities;
    }
}
