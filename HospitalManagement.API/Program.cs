using Microsoft.EntityFrameworkCore;
/* Hira Ahmad
Summary: This is the main program file for the Hospital Management API. It configures services, JWT Validation, 
and dependency injection for the application. */
using HospitalManagement.Core.Interfaces;
using HospitalManagement.Core.Models;
using HospitalManagement.Infrastructure.Data;
using HospitalManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IdentityManagement.Core.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//map the controllers and inject the dependencies


// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; // https://stackoverflow.com/questions/13510204/json-net-self-referencing-loop-detected
    }); // Required for JSON Patch support
// Configure SQLite Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


// Configure Identity 
builder.Services.AddIdentity<User, ApplicationRole>(options => 
{ 
    // Password settings 
    options.Password.RequireDigit = true; 
    options.Password.RequireLowercase = true; 
    options.Password.RequireUppercase = true; 
    options.Password.RequireNonAlphanumeric = false; 
    options.Password.RequiredLength = 6; 
    // Lockout settings 
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); 
    options.Lockout.MaxFailedAccessAttempts = 5; 
    options.Lockout.AllowedForNewUsers = true; 
    // User settings 
    options.User.RequireUniqueEmail = true; 
}) 
.AddEntityFrameworkStores<ApplicationDbContext>() 
.AddDefaultTokenProviders();

// Configure JWT Authentication 
var jwtSettings = builder.Configuration.GetSection("JwtSettings"); 
var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey is not configured");

builder.Services.AddAuthentication(options => 
{ 
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 
})
.AddJwtBearer(options => 
{ 
    options.TokenValidationParameters = new TokenValidationParameters 
    { 
        ValidateIssuer = true, 
        ValidateAudience = true, 
        ValidateLifetime = true, 
        ValidateIssuerSigningKey = true, 
        ValidIssuer = jwtSettings["Issuer"], 
        ValidAudience = jwtSettings["Audience"], 
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)) 
    }; 
});

builder.Services.AddAuthorization();

// Register Repository
builder.Services.AddScoped<IPatientRepository, PatientRepository>(); //dependency injection for patient repository
builder.Services.AddScoped<IPatientRecordRepository, PatientRecordRepository>(); //dependency injection for patient record repository
builder.Services.AddScoped<IUserRepository, UserRepository>(); //dependency injection for user repository
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>(); //dependency injection for doctor repository
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>(); //dependency injection for appointment repository
builder.Services.AddScoped<IShiftRepository, ShiftRepository>(); //dependency injection for shift repository
builder.Services.AddScoped<IReviewRepository, ReviewRepository>(); //dependency injection for review repository

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
app.MapOpenApi();
}
app.UseAuthorization();
app.MapControllers();
app.Run();