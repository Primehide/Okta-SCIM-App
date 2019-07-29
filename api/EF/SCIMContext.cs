using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.EF
{
    public class SCIMContext : DbContext
    {
        public SCIMContext()
        {

        }
        public SCIMContext(DbContextOptions<SCIMContext> options)
            :base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=52.211.18.58; database=SCIM;user=sander;password=HALO33owns**");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                  .Property(p => p.active)
                  .HasColumnType("bit");

            modelBuilder.Entity<email>()
                  .Property(p => p.primary)
                  .HasColumnType("bit");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<email> Emails { get; set; }
        public DbSet<Name> Names { get; set; }
        public DbSet<Group> Groups { get; set; }
    }
}
