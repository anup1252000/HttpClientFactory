namespace Frontend
{
    public class WeatherService : IWeatherService
    {
        private readonly ILogger<WeatherService> logger;
        private readonly HttpClient httpClient;

        public WeatherService(ILogger<WeatherService> logger,HttpClient httpClient)
        {
            this.logger = logger;
            this.httpClient = httpClient;
        }

        public async Task<string> GetWeatherAsync()
        {
            string url = $"{httpClient.BaseAddress}WeatherForecast";
            var response = await httpClient.GetAsync(url).Result.Content.ReadAsStringAsync();
            logger.LogInformation(response);
            return response;
        }
    }
}
