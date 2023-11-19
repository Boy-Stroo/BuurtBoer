using System.ComponentModel.DataAnnotations.Schema;

namespace Server_Things.Models
{
    public class OfficeDay
    {
        public Guid Id { get; init; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
    }
}
