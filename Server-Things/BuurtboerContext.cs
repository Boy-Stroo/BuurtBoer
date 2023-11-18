
using Microsoft.EntityFrameworkCore;
using Server_Things.Models;

namespace Server_Things
{
    public class BuurtboerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<DaysAtOffice> DaysAtOffice { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseNpgsql("Host=localhost;Port=5432;Database=buurtboer;Username=postgres");
            builder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Error);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(_ => _.Id);

            modelBuilder.Entity<User>()
                .HasIndex(_ => _.Email)
                .IsUnique();
            
            modelBuilder.Entity<Company>()
                .HasIndex(_ => _.Name)
                .IsUnique();
            
            modelBuilder.Entity<User>()
                .HasMany(_ => _.DaysAtOffice)
                .WithOne(_ => _.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(_ => _.Company)
                .WithMany(_ => _.Users)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Company>()
                .HasKey(_ => _.Id);

            modelBuilder.Entity<Company>()
                .HasMany(_ => _.Users)
                .WithOne(_ => _.Company)
                .HasForeignKey(_ => _.Id)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DaysAtOffice>()
                .HasKey(_ => _.Id);

            modelBuilder.Entity<DaysAtOffice>()
                .HasOne(_ => _.User)
                .WithMany(_ => _.DaysAtOffice)
                .HasForeignKey(_ => _.UserId);
        }
    }
}
