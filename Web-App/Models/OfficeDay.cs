
namespace Web_App.Models
{
    public class OfficeDay
    {
        public Guid Id { get; init; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
    }
}
