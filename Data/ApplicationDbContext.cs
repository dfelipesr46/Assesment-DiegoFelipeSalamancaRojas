using Assesment_DiegoFelipeSalamancaRojas.Models;
using Microsoft.EntityFrameworkCore;

namespace Assesment_DiegoFelipeSalamancaRojas.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSets
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Availability> Availabilities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Doctor Configurations
            modelBuilder.Entity<Doctor>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.Appointments)
                .WithOne(a => a.Doctor)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.MedicalRecords)
                .WithOne(mr => mr.Doctor)
                .HasForeignKey(mr => mr.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.Availabilities)
                .WithOne(a => a.Doctor)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Patient Configurations
            modelBuilder.Entity<Patient>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Appointments)
                .WithOne(a => a.Patient)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Patient>()
                .HasMany(p => p.MedicalRecords)
                .WithOne(mr => mr.Patient)
                .HasForeignKey(mr => mr.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Appointment Configurations
            modelBuilder.Entity<Appointment>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Appointment>()
                .Property(a => a.AppointmentDate)
                .IsRequired();

            modelBuilder.Entity<Appointment>()
                .Property(a => a.Reason)
                .HasMaxLength(500);

            // MedicalRecord Configurations
            modelBuilder.Entity<MedicalRecord>()
                .HasKey(mr => mr.Id);

            modelBuilder.Entity<MedicalRecord>()
                .Property(mr => mr.CreatedAt)
                .IsRequired();

            // Availability Configurations
            modelBuilder.Entity<Availability>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Availability>()
                .Property(a => a.StartTime)
                .IsRequired();

            modelBuilder.Entity<Availability>()
                .Property(a => a.EndTime)
                .IsRequired();

            modelBuilder.Entity<Availability>()
                .Property(a => a.IsAvailable)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
