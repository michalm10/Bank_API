using Microsoft.Extensions.DependencyInjection;
using nbp_api.Config;
using nbp_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
// Inject Services
builder.Services.AddTransient<IBankService, BankService>();
builder.Services.AddHttpClient<IBankService, BankService>();
builder.Services.Configure<BankAPI>(builder.Configuration.GetSection("BankAPI"));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
