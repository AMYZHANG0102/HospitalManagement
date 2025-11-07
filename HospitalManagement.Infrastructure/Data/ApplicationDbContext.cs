using Microsoft.EntityFrameworkCore;
using HospitalManagement.Core.Models;
namespace HospitalManagement.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Shift> Shifts { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Birthdate).IsRequired();
            entity.Property(e => e.Gender).IsRequired();
            entity.Property(e => e.HealthCard).IsRequired().HasMaxLength(12);
            entity.Property(e => e.Phone).IsRequired().HasMaxLength(12);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.HomeAddress).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Status).IsRequired();
            entity.Property(e => e.Role).IsRequired();
        });
        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Birthdate).IsRequired();
            entity.Property(e => e.Gender).IsRequired();
            entity.Property(e => e.Specialization).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Phone).IsRequired().HasMaxLength(12);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.HomeAddress).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Status).IsRequired();
            entity.Property(e => e.Role).IsRequired();
        });
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Doctor) // Appointment has one Doctor
                  .WithMany(d => d.Appointments) // Doctor can have many appointments
                  .HasForeignKey(e => e.DoctorId) // Foreign key linking Doctor
                  .IsRequired();
            entity.HasOne(e => e.Patient) // Appointment has one Patient
                  .WithMany(p => p.Appointments) // Patient can have many appointments
                  .HasForeignKey(e => e.PatientId) // Foreign key linking Patient
                  .IsRequired();
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.DateTime).IsRequired();
            entity.Property(e => e.Status).HasDefaultValue(Status.Pending);
        });
        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Doctor) // Shift has one doctor
                  .WithMany(d => d.Shifts) // Doctor can have many shifts
                  .HasForeignKey(e => e.DoctorId) // Foreign key linking Doctor
                  .IsRequired();
            entity.Property(e => e.Date).IsRequired();
            entity.Property(e => e.StartTime).IsRequired();
            entity.Property(e => e.EndTime).IsRequired();
            entity.Property(e => e.ShiftType).IsRequired();
            entity.Property(e => e.Status);      
        });
    }
}