using System;
using System.ComponentModel.DataAnnotations;
namespace ChefsNDishes.Models
{
    public class ChefDOBAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dateTime;
            if (value is DateTime)
            {
                dateTime = (DateTime)value;
            }
            else
            {
                return new ValidationResult("Invalid datetime format");
            }
            if (dateTime > DateTime.Now)
            {
                return new ValidationResult("Date cannot be in the future");
            }
            int today = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            int dob = int.Parse(dateTime.ToString("yyyyMMdd"));;
            int age = (today - dob)/10000;
            if (age < 18)
            {
                return new ValidationResult("Chef must be at least 18 years old");
            }
            return ValidationResult.Success;
        }
    }
}