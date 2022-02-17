using System;
using System.ComponentModel.DataAnnotations;

namespace Crudelicious.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }

        [Required]
        [Display(Name = "Chef's Name")]
        public string ChefName { get; set; }

        [Required]
        [Display(Name = "Name of Dish")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "# of Calories")]
        [Range(0, Int32.MaxValue, ErrorMessage = "cannot be a negative number.")]
        public int Calories { get; set; }

        [Required]
        [Range(1,5)]
        public int Tastiness { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime CreatedAt {get;set;} = DateTime.Now;

        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}