using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    [NotMapped]
    public class LoginUser
    {
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "invalid format")]  
        [Required (ErrorMessage = "is required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string LoginEmail { get; set; }

        [Required (ErrorMessage = "is required")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "must have at least 8 characters")]
        [Display(Name = "Password")]
        public string LoginPassword { get; set; }
    }
}
