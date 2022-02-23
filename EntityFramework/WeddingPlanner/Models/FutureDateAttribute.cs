using System;
using System.ComponentModel.DataAnnotations;
namespace WeddingPlanner.Models
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime date;
            if (value is DateTime)
            {
                date = (DateTime)value;
            }
            else
            {
                return new ValidationResult("Invalid date format");
            }
            if (date <= DateTime.Now)
            {
                return new ValidationResult("must be in the future");
            }
            return ValidationResult.Success;
        }
    }
}