using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required (ErrorMessage = "is required")]
        [MinLength(2, ErrorMessage = "must have at least 2 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required (ErrorMessage = "is required")]
        [MinLength(2, ErrorMessage = "must have at least 2 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "invalid format")]   
        [Required (ErrorMessage = "is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required (ErrorMessage = "is required")]
        [MinLength(8, ErrorMessage = "must have at least 8 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [Required (ErrorMessage = "is required")]
        [Compare("Password", ErrorMessage = "must match Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public List<UserWeddingRSVP> Attending { get;set; }

        public List<Wedding> Hosting { get;set; }
    }
}
