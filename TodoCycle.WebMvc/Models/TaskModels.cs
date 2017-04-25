using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoCycle.WebMvc.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Description { get; set; } // Markdown
        public bool IsComplete { get; set; }
        public Guid OwnerId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }

        // For ORM
        public Task() { }

        public Task(string description, Guid ownerId)
        {
            this.Description = description;
            this.IsComplete = false;
            this.OwnerId = ownerId;
            this.CreatedOnUtc = DateTime.UtcNow;
            this.UpdatedOnUtc = DateTime.UtcNow;
        }
    }
}