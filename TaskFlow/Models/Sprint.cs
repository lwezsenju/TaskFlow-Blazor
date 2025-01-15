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
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required.")]
        [DateGreaterThan("StartDate", ErrorMessage = "End Date must be after Start Date.")]
        public DateTime EndDate { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public List<Ticket> Tickets { get; set; }
        public string? Color { get; set; } = "#FFF1D0";
    }
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = (DateTime?)value;
            var comparisonValue = (DateTime?)validationContext.ObjectType
                .GetProperty(_comparisonProperty)
                ?.GetValue(validationContext.ObjectInstance);

            if (currentValue != null && comparisonValue != null && currentValue <= comparisonValue)
            {
                return new ValidationResult(ErrorMessage ?? "Invalid date range");
            }

            return ValidationResult.Success;
        }
    }
}
