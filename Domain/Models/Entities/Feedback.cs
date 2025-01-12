﻿using System.ComponentModel.DataAnnotations;


namespace Domain.Models.Entities
{
    public class Feedback
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        public string FeedbackText { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public virtual ICollection<FeedbackImage> FeedbackImages { get; set; }
    }

}
