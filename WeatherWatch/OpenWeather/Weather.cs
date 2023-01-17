using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherWatch.OpenWeather
{
    internal class Weather
    {
        public int id;

        public string main;

        public string description;

        public string icon;

        public Bitmap Icon
        {
            get
            {
                return new Bitmap(Image.FromFile($"icons/{icon}.png"));
            }
        } 
    }
}
