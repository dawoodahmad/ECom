using ECom.Api.Search.Interfaces;
using ECom.Api.Search.Services;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var provider = builder.Services.BuildServiceProvider();
var _configuration = provider.GetRequiredService<IConfiguration>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddHttpClient("OrdersService", conf =>
{
    conf.BaseAddress = new Uri(_configuration.GetSection("Services").GetValue<string>("Orders"));
});

builder.Services.AddHttpClient("ProductsService", conf =>
{
    conf.BaseAddress = new Uri(_configuration.GetSection("Services").GetValue<string>("Products"));
});

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
