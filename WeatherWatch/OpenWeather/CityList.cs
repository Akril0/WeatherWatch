using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherWatch.OpenWeather
{
     public class CityList
    {
        public string[] cityList;
        public CityList() {

            cityList= new string[0];
            ReadFile();

         }
        
        private async void ReadFile()
        {
            string path = "../../../Cities/cityList.txt";

            // асинхронное чтение
            using (StreamReader reader = new StreamReader(path))
            {
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    AddCyti(ref cityList,line);
                }
            }
        }

        private void AddCyti(ref string[] list, string name)
        {
            string[] newList = new string[list.Length + 1];
            for (int i = 0; i < list.Length; i++) {
                newList[i] = list[i];
            }
            newList[list.Length] = name;
            list = newList;
        }

        public string[] Sort(string text)
        {
           string[] newlist = new string[0];
            for(int i =0; i < cityList.Length; i++)
            {
                if (cityList[i].StartsWith(text) ){
                    AddCyti(ref newlist, cityList[i]);
                }
            }
            return newlist;
        } 
    }
}
