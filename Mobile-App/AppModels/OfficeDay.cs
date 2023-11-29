    using System.ComponentModel.DataAnnotations.Schema;
    namespace Mobile_App
    {
        public class OfficeDay
        {
            public Guid Id { get; init; }
            public Guid UserId { get; set; }
            public User User { get; set; }
            public DateTime Date { get; set; }
        }
    }
