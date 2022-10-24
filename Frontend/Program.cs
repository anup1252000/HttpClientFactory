using Frontend;

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
builder.Services.AddHttpClient<IWeatherService,WeatherService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7054");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});
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
