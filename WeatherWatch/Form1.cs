using Newtonsoft.Json;
using System.Net;
using WeatherWatch.OpenWeather;

namespace WeatherWatch
{
    public partial class Form1 : Form
    {
        CityList cityList = new CityList();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(cityList.cityList);
            getWeather((string)comboBox1.Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            getWeather((string)comboBox1.SelectedItem);
        }

        private async void getWeather(string city)
        {
            try { 
            
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

            label3.Text = "������� ����������� (��): " + oW.main.temp.ToString("0.##");

            label4.Text = "��������� (%): " + oW.main.humidity.ToString();

            label5.Text = "�������� (��): " + ((int)oW.main.pressure).ToString();

            label6.Text = "�������� (�/�): " + oW.wind.speed.ToString();

            label7.Text = "����������� (�): " + oW.wind.deg.ToString();
            }catch(Exception ex)
            {
                label1.Text = "������";
            }
        }
        private void comboBox1_TextUpdate (object sender, EventArgs e)
        {
            string text = comboBox1.Text;
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(cityList.Sort(text));
            comboBox1.SelectionStart = comboBox1.Text.Length;
        }
    }
}