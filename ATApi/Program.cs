using ATApi.Data.Models;
using ATApi.Repo.ConnectionFactory;
using ATApi.Repo.Repositories;
using ATApi.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IVehicleService<Vehicle>, VehicleService>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IConnFactory, ConnFactory>();
builder.Services.AddScoped<VehicleContext>();

// Add DBContext to the container.
builder.Services.AddDbContext<UserContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("VehicleDb"));
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {
    Title = "ATApi",
    Version = "v1",
    Description = "An ASP.NET Core Web API for managing AutoTrader data",
    });
    //c.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
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
