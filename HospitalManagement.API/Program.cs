using Microsoft.EntityFrameworkCore;
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

builder.Services.AddControllers().AddNewtonsoftJson(); // For JSON Patch
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
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IShiftRepository, ShiftRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
app.MapOpenApi();
}
app.UseAuthorization();
app.MapControllers();
app.Run();