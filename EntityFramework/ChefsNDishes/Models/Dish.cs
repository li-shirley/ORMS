using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefsNDishes.Models
{
    public class Dish
    {
        // Primary Key
        [Key] 
        public int DishId { get; set; }


        [Required(ErrorMessage = "is required")]
        [Display(Name = "Name of Dish")]
        public string Name { get; set; }


        [Required(ErrorMessage = "is required")]
        [Range(0, Int32.MaxValue, ErrorMessage = "cannot be a negative number.")]
        [Display(Name = "# of Calories")]
        public int Calories { get; set; }


        [Required(ErrorMessage = "is required")]
        public string Description { get; set; }


        [Range(1, 5)]
        public int Tastiness { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;


        public DateTime UpdatedAt { get; set; } = DateTime.Now;


        // Foreign Key
        [Display(Name = "Chef")]
        public int ChefId { get; set; }
        // Navigation Property. Allows navigation of two entity types (i.e. a dish's chef). Need to use .Include in query to access
        public Chef Cook { get; set; }
    }
}