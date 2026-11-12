using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CureHospitalDALCrossPlatform.Models;

public class CureHospitalDbContext : DbContext
{
    public CureHospitalDbContext()
    {
    }

    public CureHospitalDbContext(DbContextOptions<CureHospitalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    public virtual DbSet<DoctorSpecialization> DoctorSpecializations { get; set; }

    public virtual DbSet<Surgery> Surgeries { get; set; }

    public virtual DbSet<SurgeryDetail> SurgeryDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }

        var basePath = Directory.GetCurrentDirectory();
        var settingsFile = "EduAppsettings.json";
        if (!File.Exists(Path.Combine(basePath, settingsFile)))
        {
            settingsFile = "appsettings.json";
        }

        if (!File.Exists(Path.Combine(basePath, settingsFile)))
        {
            basePath = AppContext.BaseDirectory;
            settingsFile = File.Exists(Path.Combine(basePath, "EduAppsettings.json"))
                ? "EduAppsettings.json"
                : "appsettings.json";
        }

        var config = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile(settingsFile)
            .Build();
        optionsBuilder.UseSqlServer(config.GetConnectionString("DBConnectionString"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("pk_DoctorID");
            entity.ToTable("Doctor");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.DoctorName).IsRequired().HasMaxLength(50).IsUnicode(false);
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(e => e.SpecializationCode).HasName("pk_SpecializationCode");
            entity.ToTable("Specialization");
            entity.Property(e => e.SpecializationCode).HasMaxLength(3).IsUnicode(false).IsFixedLength();
            entity.Property(e => e.SpecializationName).IsRequired().HasMaxLength(50).IsUnicode(false);
        });

        modelBuilder.Entity<DoctorSpecialization>(entity =>
        {
            entity.HasKey(e => new { e.DoctorId, e.SpecializationCode }).HasName("pk_DoctorIDSpecializationCode");
            entity.ToTable("DoctorSpecialization");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.SpecializationCode).HasMaxLength(3).IsUnicode(false).IsFixedLength();
            entity.Property(e => e.SpecializationDate).HasColumnType("date");
            entity.HasOne(d => d.Doctor).WithMany(p => p.DoctorSpecializations)
                .HasForeignKey(d => d.DoctorId).HasConstraintName("fk_DoctorID");
            entity.HasOne(d => d.SpecializationCodeNavigation).WithMany(p => p.DoctorSpecializations)
                .HasForeignKey(d => d.SpecializationCode).HasConstraintName("fk_SpecializationCode");
        });

        modelBuilder.Entity<Surgery>(entity =>
        {
            entity.HasKey(e => e.SurgeryId).HasName("pk_SurgeryID");
            entity.ToTable("Surgery");
            entity.Property(e => e.SurgeryId).HasColumnName("SurgeryID");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.SurgeryDate).HasColumnType("date");
            entity.Property(e => e.StartTime).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.EndTime).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.SurgeryCategory).HasMaxLength(3).IsUnicode(false).IsFixedLength();
            entity.HasOne(d => d.Doctor).WithMany(p => p.Surgeries)
                .HasForeignKey(d => d.DoctorId).HasConstraintName("fk_DoctorID_Surgery");
            entity.HasOne(d => d.SurgeryCategoryNavigation).WithMany(p => p.Surgeries)
                .HasForeignKey(d => d.SurgeryCategory).HasConstraintName("fk_SpecializationCode_Surgery");
        });

        modelBuilder.Entity<SurgeryDetail>(entity =>
        {
            entity.HasNoKey();
            entity.Property(e => e.SurgeryId).HasColumnName("SurgeryID");
            entity.Property(e => e.SurgeryDate).HasColumnType("date");
            entity.Property(e => e.StartTime).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.EndTime).HasColumnType("decimal(4, 2)");
        });
    }
}
