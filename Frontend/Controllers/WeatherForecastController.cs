using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {


        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherService weatherService;
        private readonly IHttpClientFactory _httpClientFactory;


        #region Uncomment this for named httpclientfactory
        //public WeatherForecastController(ILogger<WeatherForecastController> logger,IHttpClientFactory httpClientFactory)
        //{
        //    _logger = logger;
        //    _httpClientFactory = httpClientFactory;
        //}
        #endregion

        #region Uncomment this for typed httpclientfactory
        public WeatherForecastController(ILogger<WeatherForecastController> logger,IWeatherService weatherService)
        {
            _logger = logger;
            this.weatherService = weatherService;
        }
        #endregion

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task Get()
        {
            for (int i = 0; i < 100; i++)
            {
                #region Norml HttpClient
                //using var httpClient = new HttpClient();
                //var response = await httpClient.GetAsync("https://localhost:7054/WeatherForecast").Result.Content.ReadAsStringAsync();
                #endregion


                #region Named HttpClientFactory
                //using var httpClient = _httpClientFactory.CreateClient("weatherForecast");
                //string url = $"{httpClient.BaseAddress}WeatherForecast";
                //var response = await httpClient.GetAsync(url).Result.Content.ReadAsStringAsync();
                //_logger.LogInformation(response);
                #endregion

                #region Typed HttpClientFactory
                var response = await weatherService.GetWeatherAsync();
                _logger.LogInformation(response);
                #endregion
            }
        }
    }
}