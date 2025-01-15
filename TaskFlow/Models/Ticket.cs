using System.ComponentModel.DataAnnotations;
namespace TaskFlow.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Range(1, 100, ErrorMessage = "Points must be between 1 and 100.")]
        public int Points { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public TicketStatus Status { get; set; } = TicketStatus.Offen;

        public int? SprintId { get; set; }
        public Sprint? Sprint { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
    public enum TicketStatus
    {
        Offen,
        InBearbeitung,
        Erledigt
    }
}
