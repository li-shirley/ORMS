using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefsNDishes.Models
{
    public class Chef
    {
        [Key]
        public int ChefId { get; set; }


        [Required(ErrorMessage = "is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "is required")]
        [ChefDOB]
        [Column(TypeName = "date")]
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        

        // Navigation Property. Allows navigation of two entity types (i.e. a chef's dishes). Need to use .Include in query to access
        public List<Dish> Dishes { get; set; }

        public string FullName()
        {
            return FirstName + " " + LastName;
        }
    }
}