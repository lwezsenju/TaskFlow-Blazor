using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Models
{
    public class DateGreaterThan : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateGreaterThan(string comparisonProperty)
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
