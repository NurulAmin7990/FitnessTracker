using FitnessTracker.BusinessLogic.Data;
using FitnessTracker.BusinessLogic.Interfaces;
using FitnessTracker.BusinessLogic.Models;
using FitnessTracker.WebAPI.Filters;
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
        TermsOfService = new Uri("https://fitnesstracker.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Nurul Amin",
            Email = "NurulAmin7990@gmail.com"
        }
    });
    options.EnableAnnotations();
    options.SchemaFilter<SwaggerSchemaExampleFilter>();
});
builder.Services.AddDbContext<FitnessTrackerContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddCors(options => options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
// Add local services to the container
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Configure the HTTP request pipeline.
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    options.SwaggerEndpoint("/swagger/v1/swagger.json",
    "Fitness Tracker API v1"));
    app.UseReDoc(options =>
    {
        options.DocumentTitle = "Fitness Tracker Documentation";
        options.SpecUrl = "/swagger/v1/swagger.json";
    });
}
app.UseAuthorization();
app.MapControllers();
app.UseCors("Open");
app.Run();
app.UseSwagger(options =>
{
    //options.SerializeAsV2 = true;
});
