using System;
using System.ComponentModel.DataAnnotations;
namespace Azurenet.Models
{

        public class User
        {
            [Key]
            public int UserId { get; set; }

            [Required]
            [EmailAddress]
            [StringLength(100)]
            public string Email { get; set; } = string.Empty;
         
            [Required]
            public string PasswordHash { get; set; } = string.Empty;
            [Required]
            public DateTime CreatedAt { get; set; }


            public string? ResetToken { get; set; } = string.Empty;

            public string? Role { get; set; } = string.Empty;

            public virtual ICollection<Order>? Orders { get; set; } = null;

            public virtual ICollection<CartItem>? CartItems { get; set; } = null;
        }
    }
