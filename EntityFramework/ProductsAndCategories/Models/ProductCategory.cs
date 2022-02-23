using System;
using System.ComponentModel.DataAnnotations;

namespace ProductsAndCategories.Models
{
    public class ProductCategory
    {
        public int ProductCategoryId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;


        [Display(Name = "Product")]
        public int ProductId { get; set; }
        
        [Display(Name = "Category")]
        public int CategoryId { get; set; }



        public Product Product { get; set; }

        public Category Category { get; set; }


    }
}