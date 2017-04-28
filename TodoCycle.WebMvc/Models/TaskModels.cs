using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TodoCycle.WebMvc.Models
{
    public class Task
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; } // Markdown

        public bool IsComplete { get; set; }
        public string OwnerId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }

        // For ORM
        public Task() { }

        public Task(string description, string ownerId)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException(nameof(description));
            }

            if (string.IsNullOrWhiteSpace(ownerId))
            {
                throw new ArgumentException(nameof(ownerId));
            }

            this.Description = description;
            this.IsComplete = false;
            this.OwnerId = ownerId;
            this.CreatedOnUtc = DateTime.UtcNow;
            this.UpdatedOnUtc = DateTime.UtcNow;
        }
    }
}