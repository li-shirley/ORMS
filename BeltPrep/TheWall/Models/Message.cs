using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TheWall.Models;

namespace TheWall.Models
{
    public class Message
    {
        [Key] 
        public int MessageId { get; set; }
        
        [Required (ErrorMessage = "is required")]
        [MinLength(10, ErrorMessage = "must have at least 10 characters")]
        public string Body { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public int UserId { get; set; }

        public List<Comment> Comments { get;set; }
    }
}