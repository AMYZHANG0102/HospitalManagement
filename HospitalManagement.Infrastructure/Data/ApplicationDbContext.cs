/* Amy Zhang
Summary: This class represents the application's database context.
It defines the DbSets for each entity and configures their relationships. */

using Microsoft.EntityFrameworkCore;
using HospitalManagement.Core.Models;
using IdentityManagement.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace HospitalManagement.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<User, ApplicationRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }

    #region DbSets
    public DbSet<User> Users { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<PatientRecord> PatientRecords { get; set; }
    public DbSet<RecordEntry> RecordEntries { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<Review> Reviews { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Use lambda (e => e.NavigationProperty) when the entity has a navigation property for that relationship.
        // Use the generic type (HasMany<EntityType>()) when the entity does not have a navigation property.

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Role).IsRequired();
            entity.Property(e => e.Phone).IsRequired().HasMaxLength(12);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.Gender).IsRequired();
            entity.Property(e => e.Birthdate).IsRequired();
            entity.Property(e => e.HomeAddress).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Status).IsRequired();

        });

        #region Table-Per-Type Inheritance Mapping (Extended User Types)
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasBaseType<User>(); // Patient inherits from User
            entity.Property(e => e.HealthCard).IsRequired().HasMaxLength(12);
            // Configure one-to-many relationships:
            entity.HasMany(e => e.Appointments) // Patient can have many appointments
                  .WithOne(a => a.Patient); // Navigation property on Appointment entity
            entity.HasMany(e => e.ReviewsSent) // Patient can send many reviews
                  .WithOne(r => r.Patient); // Navigation property on Review entity
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasBaseType<User>(); // Doctor inherits from User
            entity.Property(e => e.Specialization).IsRequired();
            // Configure one-to-many relationships:
            entity.HasMany(e => e.Appointments) // Doctor can have many appointments
                  .WithOne(a => a.Doctor); // Navigation property on Appointment entity   
            entity.HasMany(e => e.ReviewsReceived) // Doctor can receive many reviews
                  .WithOne(r => r.Doctor); // Navigation property on Review entity
            // Configure many-to-many relationship:
            entity.HasMany(e => e.Shifts) // Doctor can have many shifts
                  .WithMany(s => s.Doctors); // Shift can have many doctors

         });
        #endregion

        modelBuilder.Entity<PatientRecord>(entity =>
        {
            entity.HasKey(e => e.Id);
            // Configure one-to-one relationship:
            entity.HasOne(e => e.Patient) // PatientRecord is tied to one Patient only
                  .WithOne(p => p.PatientRecord) // Navigation property on Patient entity
                  .HasForeignKey<PatientRecord>(e => e.PatientId) // Foreign key in PatientRecord
                  .IsRequired();
            // Configure one-to-many relationship:      
            entity.HasMany(e => e.Entries) // One PatientRecord has many RecordEntries
                  .WithOne(re => re.PatientRecord); // Navigation property in RecordEntry
        });

        modelBuilder.Entity<RecordEntry>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.DateTime);
            // Configure many-to-one relationship:
            entity.HasOne(e => e.PatientRecord) // Each RecordEntry belongs to one PatientRecord
                  .WithMany(pr => pr.Entries) // PatientRecord has many RecordEntries
                  .HasForeignKey(e => e.PatientRecordId) // Foreign key in RecordEntry
                  .IsRequired();
            entity.HasOne(e => e.Doctor) // Each RecordEntry is created by one Doctor
                  .WithMany() // No navigation property in Doctor for RecordEntries
                  .HasForeignKey(e => e.DoctorId) // Foreign key in RecordEntry
                  .IsRequired();     
        });
        
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.DateTime).IsRequired();
            entity.Property(e => e.Status).HasDefaultValue(AppointmentStatus.Pending);
            // Configure many-to-one relationships:
            entity.HasOne(e => e.Doctor) // Appointment has one Doctor
                  .WithMany(d => d.Appointments) // Doctor can have many appointments
                  .HasForeignKey(e => e.DoctorId) // Foreign key linking Doctor
                  .IsRequired();
            entity.HasOne(e => e.Patient) // Appointment has one Patient
                  .WithMany(p => p.Appointments) // Patient can have many appointments
                  .HasForeignKey(e => e.PatientId) // Foreign key linking Patient
                  .IsRequired();
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Date).IsRequired();
            entity.Property(e => e.StartTime).IsRequired();
            entity.Property(e => e.EndTime).IsRequired();
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.Status);
            // Configure many-to-many relationship:
            entity.HasMany(e => e.Doctors) // Shift has one doctor
                  .WithMany(d => d.Shifts); // Doctor can have many shifts
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Rating).IsRequired();
            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.Property(e => e.Date).IsRequired();
            // Configure many-to-one relationships:
            entity.HasOne(e => e.Patient) // Review is sent by one Patient
                    .WithMany(p => p.ReviewsSent) // Patient can send many reviews
                    .HasForeignKey(e => e.PatientId) // Foreign key linking Patient
                    .IsRequired();
            entity.HasOne(e => e.Doctor) // Review can be about a one Doctor
                    .WithMany(d => d.ReviewsReceived) // Doctor can receive many reviews
                    .HasForeignKey(e => e.DoctorId); // Foreign key linking Doctor
        });

        modelBuilder.Entity<ApplicationRole>(entity=>
        {
            entity.ToTable("Roles");
        });
    }
}