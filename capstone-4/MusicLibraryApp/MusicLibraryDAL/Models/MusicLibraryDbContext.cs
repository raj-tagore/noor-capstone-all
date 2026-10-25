using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MusicLibraryDAL.Models;

public partial class MusicLibraryDbContext : DbContext
{
    public MusicLibraryDbContext()
    {
    }

    public MusicLibraryDbContext(DbContextOptions<MusicLibraryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }

        var basePath = Directory.GetCurrentDirectory();
        if (!File.Exists(Path.Combine(basePath, "appsettings.json")))
        {
            basePath = AppContext.BaseDirectory;
        }

        var builder = new ConfigurationBuilder()
                      .SetBasePath(basePath)
                      .AddJsonFile("appsettings.json");
        var config = builder.Build();
        var connectionString = config.GetConnectionString("MusicLibraryDBConnectionString");
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.AlbumName).HasName("pk_AlbumName");

            entity.Property(e => e.AlbumName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.HasKey(e => e.SongId).HasName("pk_SongId");

            entity.Property(e => e.SongName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasIndex(e => e.SongName).IsUnique();

            entity.Property(e => e.AlbumName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Album).WithMany(p => p.Songs)
                .HasForeignKey(d => d.AlbumName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_AlbumName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
