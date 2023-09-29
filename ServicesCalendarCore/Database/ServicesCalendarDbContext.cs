using Microsoft.EntityFrameworkCore;
using ServicesCalendarCore.Models;

namespace ServicesCalendarCore.Database
{
    public class ServicesCalendarDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Quota> Quotas { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=CalendarAppData.db");
        }
    }
}
