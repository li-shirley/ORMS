using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginAndRegistration.Models
{
    [NotMapped]
    public class LoginUser
    {
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
