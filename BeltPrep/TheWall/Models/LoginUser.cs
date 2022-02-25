using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheWall.Models
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
        [Display(Name = "Password")]
        public string LoginPassword { get; set; }
    }
}
