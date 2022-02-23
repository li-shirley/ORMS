using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsAndCategories.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required (ErrorMessage = "is required")]
        [MinLength(2, ErrorMessage = "must have at least 2 characters")]
        public string Name { get; set; }

        [Required (ErrorMessage = "is required")]
        [MinLength(10, ErrorMessage = "must have at least 10 characters")]
        public string Description { get; set; }

        [Required (ErrorMessage = "is required")]
        [Range(0, Int32.MaxValue, ErrorMessage = "cannot be negative")]
        public double? Price { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;


        public List<ProductCategory> ProductCategories { get; set; }
    }
}
