using Newtonsoft.Json;
using System.Net;
namespace WeatherWatch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getWeather((string)comboBox1.SelectedItem);
        }

        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            getWeather((string)comboBox1.SelectedItem);
        }

        private async void getWeather(string city)
        {
            WebRequest request = WebRequest.Create($"https://api.openweathermap.org/data/2.5/weather?q={city}&appid=eece318604b63a7abd9487d1849d9170");
            request.Method = "POST";

            request.ContentType = "application/x-www-urlencoded";

            WebResponse response = await request.GetResponseAsync();

            string answer = String.Empty;

            using (Stream s = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(s))
                {
                    answer = await reader.ReadToEndAsync();
                }
            }
            response.Close();

            OpenWeather.OpenWeather oW = JsonConvert.DeserializeObject<OpenWeather.OpenWeather>(answer);

            panel1.BackgroundImage = oW.weather[0].Icon;

            label1.Text = oW.weather[0].main;

            label2.Text = oW.weather[0].description;

            label3.Text = "Средняя температура (°С): " + oW.main.temp.ToString("0.##");

            label4.Text = "Влажность (%): " + oW.main.humidity.ToString();

            label5.Text = "Давление (мм): " + ((int)oW.main.pressure).ToString();

            label6.Text = "Скорость (м/с): " + oW.wind.speed.ToString();

            label7.Text = "Направление (°): " + oW.wind.deg.ToString();
        }
    }
}