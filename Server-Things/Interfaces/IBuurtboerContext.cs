using Microsoft.EntityFrameworkCore;
using Server_Things.Models;

namespace Server_Things.Interfaces
{
    public interface IBuurtboerContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<OfficeDay> OfficeDays { get; set; }

        public Task<int> SaveChangesAsync();

    }
}