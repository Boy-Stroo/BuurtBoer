using Microsoft.EntityFrameworkCore;
using Server_Things.Models;

namespace Server_Things
{
    public class BuurtboerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<OfficeDay> OfficeDays { get; set; }
        public DbSet<Grocery> GroceryList { get; set; }

        public BuurtboerContext()
        {
            
        }

        public BuurtboerContext(DbContextOptions<BuurtboerContext> options) : base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseNpgsql("Host=localhost;Port=5432;Database=BuurtBoer;Username=postgres;Include Error Detail=true");
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

            modelBuilder.Entity<Grocery>()
                .HasKey(_ => _.Id);

            modelBuilder.Entity<Grocery>()
                .HasOne(_ => _.Company)
                .WithMany(_ => _.ListOfGroceries)
                .HasForeignKey(_ => _.CompanyID);

            modelBuilder.Entity<Company>()
                .HasMany(_ => _.ListOfGroceries)
                .WithOne(_ => _.Company)
                .OnDelete(DeleteBehavior.Cascade);

        }

        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();
    }
}
