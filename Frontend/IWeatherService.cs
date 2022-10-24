namespace Frontend
{
    public interface IWeatherService
    {
        Task<string> GetWeatherAsync();
    }
}
