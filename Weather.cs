using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace ATM_Software
{
    public partial class Weather : Form
    {
        private const string ApiKey = "6f3f784295787ab1b8a30793dec7c745";
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";

        public Weather()
        {
            InitializeComponent();
        }

        private async void Weather_Load(object sender, EventArgs e)
        {
            var weatherInfo = await GetWeatherInfo("London");
            if (weatherInfo != null)
            {
                label7.Text = weatherInfo.Detail;
                label8.Text = $"{weatherInfo.Temperature}°C";
            }
        }

        public async Task<WeatherInfo> GetWeatherInfo(string cityName)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string url = $"{BaseUrl}?q={cityName}&appid={ApiKey}&units=metric";
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                        WeatherInfo weatherInfo = new WeatherInfo
                        {
                            Temperature = result.main.temp,
                            Conditions = result.weather[0].main,
                            Detail = result.weather[0].description,
                            WeatherIcon = result.weather[0].icon,
                            WindSpeed = result.wind.speed,
                            Pressure = result.main.pressure
                        };

                        return weatherInfo;
                    }
                    else
                    {
                        MessageBox.Show($"Failed to retrieve weather data. Status code: {response.StatusCode}");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                    return null;
                }
            }
        }

        private async void loginbtn_Click(object sender, EventArgs e)
        {
            string city = txt1.Text;
            if (string.IsNullOrEmpty(city))
            {
                MessageBox.Show("Please enter a city name.");
                return;
            }

            var weatherInfo = await GetWeatherInfo(city);
            if (weatherInfo != null)
            {
                label7.Text = weatherInfo.Detail;
                label8.Text = $"{weatherInfo.Temperature}°C";
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void txt1_TextChanged(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

    public class WeatherInfo
    {
        public double Temperature { get; set; }
        public string Conditions { get; set; }
        public string Detail { get; set; }
        public string WeatherIcon { get; set; }
        public double WindSpeed { get; set; }
        public int Pressure { get; set; }
    }
}
