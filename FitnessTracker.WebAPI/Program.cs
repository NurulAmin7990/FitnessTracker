using ElmahCore.Mvc;
using ElmahCore.Sql;
using FitnessTracker.BusinessLogic.Data;
using FitnessTracker.BusinessLogic.Interfaces;
using FitnessTracker.BusinessLogic.Models;
using FitnessTracker.WebAPI.Filters;
using FitnessTracker.WebAPI.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var graphqlUri = "https://fitnesstracker.com/graphql";

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Fitness Tracker API",
        Description = "An ASP.NET Core Web API for fitness tracking and workout management. GraphQL: " + graphqlUri,
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
builder.Services.AddGraphQLServer().AddQueryType<Query>().AddProjections().AddFiltering().AddSorting();
builder.Services.AddElmah<SqlErrorLog>(options =>
{
    //options.OnPermissionCheck = context => context.User.Identity.IsAuthenticated;
    options.ConnectionString = connectionString;
    //options.SqlServerDatabaseSchemaName = "Errors";
    options.SqlServerDatabaseTableName = "ElmahError";
});
// Add local services to the container
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Configure the HTTP request pipeline.
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    graphqlUri = "http://localhost:5023/graphql";

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
//app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseCors("Open");
app.UseSwagger(options =>
{
    //options.SerializeAsV2 = true;
});
app.MapGraphQL("/graphql");
app.UseElmah();
app.Run();
