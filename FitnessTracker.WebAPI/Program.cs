using FitnessTracker.BusinessLogic.Data;
using FitnessTracker.BusinessLogic.Interfaces;
using FitnessTracker.BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Fitness Tracker API",
        Description = "An ASP.NET Core Web API for fitness tracking and workout management",
        TermsOfService = new Uri("https://fitnesstracker.com/terms")
    });
});
builder.Services.AddDbContext<FitnessTrackerContext>(options =>
{
    options.UseSqlServer(connectionString);
});
// Add local services to the container
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Configure the HTTP request pipeline.
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();
app.MapControllers();
app.Run();
app.UseSwagger(options =>
{
    //options.SerializeAsV2 = true;
});
