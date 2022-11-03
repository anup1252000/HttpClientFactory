using Frontend;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Uncomment this for Named httpclientfactory
//builder.Services.AddHttpClient("weatherForecast", client =>
//{
//    client.BaseAddress = new Uri("https://localhost:7054");
//    client.DefaultRequestHeaders.Add("Accept", "application/json");
//});
#endregion

#region Uncomment this for typed httpclientfactory
var bulk = Policy.BulkheadAsync<HttpResponseMessage>(3, 5, x =>
{
    Console.WriteLine("rejected"+x.OperationKey);
    return Task.CompletedTask;
});
builder.Services.AddHttpClient<IWeatherService, WeatherService>(client =>
 {
     client.BaseAddress = new Uri("https://localhost:7054");
     client.DefaultRequestHeaders.Add("Accept", "application/json");
 })
    .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(2)))
    .AddTransientHttpErrorPolicy(policy => policy.CircuitBreakerAsync(5, TimeSpan.FromSeconds(10)))
    .AddPolicyHandler(policy => bulk);
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
