using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WeddingPlanner.Models;

namespace WeddingPlanner.Models
{
    public class Wedding
    {
        [Key] 
        public int WeddingId { get; set; }
        
        [Required (ErrorMessage = "is required")]
        [MinLength(2, ErrorMessage = "must have at least 2 characters")]
        public string WedderOne { get; set; }

        [Required (ErrorMessage = "is required")]
        [MinLength(2, ErrorMessage = "must have at least 2 characters")]
        public string WedderTwo { get; set; }

        [Required(ErrorMessage = "is required")]
        [DataType(DataType.Date)]
        [FutureDate]
        public DateTime? Date { get; set; }

        [Display(Name = "Wedding Address")]
        [Required (ErrorMessage = "is required")]
        public string Address { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public int UserId { get; set; }

        public User Host { get;set; }
        public List<UserWeddingRSVP> Attendees { get;set; }
    }
}