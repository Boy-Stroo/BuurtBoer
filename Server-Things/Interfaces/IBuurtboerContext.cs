using Microsoft.EntityFrameworkCore;
using Server_Things.Models;

public interface IBuurtboerContext
{
    DbSet<User> Users { get; set; }
    DbSet<Company> Companies { get; set; }
    DbSet<OfficeDay> OfficeDays { get; set; }
}