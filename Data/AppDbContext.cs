using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Data
{
    public class EfDbContext : DbContext
    {
        public EfDbContext(DbContextOptions<EfDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<SchemaVersion> SchemaVersions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLowerCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("users")
                .HasKey(u => u.Id);

            modelBuilder.Entity<Permission>()
                .ToTable("permissions")
                .HasKey(p => p.Id);

            modelBuilder.Entity<SchemaVersion>()
                .ToTable("schema_version")
                .HasKey(s => s.Id);

            base.OnModelCreating(modelBuilder);
        }
    }

}
