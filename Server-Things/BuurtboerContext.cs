using Microsoft.EntityFrameworkCore;
using Server_Things.Models;

namespace Server_Things
{
    public class BuurtboerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<OfficeDay> OfficeDays { get; set; }

        private readonly string _options;

        public BuurtboerContext(string options)
        {
            _options = options;
        }

        public BuurtboerContext()
        {
            _options = "Host=localhost;Port=5432;Database=buurtboer;Username=postgres;Include Error Detail=true";
        }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseNpgsql(_options);
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
                .HasForeignKey(_ => _.CompanyId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Company>()
                .HasKey(_ => _.Id);

            modelBuilder.Entity<Company>()
                .HasMany(_ => _.Users)
                .WithOne(_ => _.Company)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OfficeDay>()
                .HasKey(_ => _.Id);

            modelBuilder.Entity<OfficeDay>()
                .HasOne(_ => _.User)
                .WithMany(_ => _.DaysAtOffice)
                .HasForeignKey(_ => _.UserId);
        }
    }
}
