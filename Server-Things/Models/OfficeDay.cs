using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server_Things.Models
{
    public class OfficeDay
    {
        public Guid Id { get; init; }
        [Required]
        public Guid UserId { get; set; }
        public User? User { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        public OfficeDay(Guid userId, DateOnly date)
        {
            UserId = userId;
            Date = date;
            Id = Guid.NewGuid();
        }
    }
}
