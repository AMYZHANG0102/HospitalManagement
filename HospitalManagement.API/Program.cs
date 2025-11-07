using Microsoft.EntityFrameworkCore;
using HospitalManagement.Core.Interfaces;
using HospitalManagement.Core.Models;
using HospitalManagement.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//map the controllers and inject the dependencies


// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(); // Required for JSON Patch support
// Configure SQLite Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
// Register Repository
builder.Services.AddScoped<IPatientRepository, PatientRepository>(); //dependency injection for patient repository
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
app.MapOpenApi();
}
app.UseAuthorization();
app.MapControllers();
app.Run();