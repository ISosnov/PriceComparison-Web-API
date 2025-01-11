﻿
using System.ComponentModel.DataAnnotations;


namespace Domain.Models.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; }

        public virtual ICollection<Seller> Sellers { get; set; }
    }

}
