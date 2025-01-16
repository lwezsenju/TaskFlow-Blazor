using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Models
{
    public class Sprint
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Start Date is required.")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "End Date is required.")]
        [DateGreaterThan("StartDate", ErrorMessage = "End Date must be after Start Date.")]
        public DateTime EndDate { get; set; } = DateTime.Now;
        public int? UserId { get; set; }
        public User? User { get; set; }
        public List<Ticket> Tickets { get; set; }
        public string? Color { get; set; } = "#FFF1D0";
    }
    
}
