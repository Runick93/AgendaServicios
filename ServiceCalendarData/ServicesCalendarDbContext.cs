using Microsoft.EntityFrameworkCore;
using ServicesCalendarData.Models;
using System.Reflection;

namespace ServicesCalendarData
{
    public class ServicesCalendarDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Quota> Quotas { get; set; }

        //public ServicesCalendarDbContext(DbContextOptions<ServicesCalendarDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString: "Filename= AppData.db",
                sqliteOptionsAction: op =>
                {
                    op.MigrationsAssembly(
                        Assembly.GetExecutingAssembly().FullName
                        );
                });
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("addresses");
                entity.HasKey(e => e.Id);
                entity.HasOne(a => a.User)
                    .WithMany(u => u.Addresses)
                    .HasForeignKey(a => a.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("services");
                entity.HasKey(e => e.Id);
                entity.HasOne(s => s.Address)
                    .WithMany(a => a.Services)
                    .HasForeignKey(s => s.AddressId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Quota>(entity =>
            {
                entity.ToTable("quotas");
                entity.HasKey(e => e.Id);
                entity.HasOne(q => q.Service)
                    .WithMany(s => s.Quotas)
                    .HasForeignKey(q => q.ServiceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
