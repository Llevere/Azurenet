using System;
using System.ComponentModel.DataAnnotations;
namespace Azurenet.Models
{

        public class User
        {
            // Unique identifier for the user
            [Key]
            public int UserId { get; set; }

            // Username for login
           // [Required]
           // [StringLength(50)]
           // public string Username { get; set; } = string.Empty;

            // Email address
            [Required]
            [EmailAddress]
            [StringLength(100)]
            public string Email { get; set; } = string.Empty;
         
            // Password hash (we never store the plain password)
            [Required]
            public string PasswordHash { get; set; } = string.Empty;


            // User's full name
           // [StringLength(100)]
           // public string FullName { get; set; } = string.Empty;

            // User's phone number (optional)
            //[StringLength(20)]
            //public string? PhoneNumber { get; set; } = null;

            // User's shipping address (optional)
            //public string ShippingAddress { get; set; } = string.Empty;

            // User's billing address (optional)
            //public string BillingAddress { get; set; } = string.Empty;

            // Date of birth (optional)
            //public DateTime? DateOfBirth { get; set; }

            // Role of the user (Admin, Customer, etc.)
           // [Required]
            //public string Role { get; set; } = string.Empty;

            // User's account creation date
            [Required]
            public DateTime CreatedAt { get; set; }

            // User's last login date (useful for auditing and activity tracking)
            //public DateTime? LastLogin { get; set; }

            // Is the user account verified via email or not
            //[Required]
            //public bool IsVerified { get; set; } = false;

            // Indicates if the user has confirmed their email address
            //public bool EmailConfirmed { get; set; } = false;

            // A token or identifier used for password reset or email verification
            public string? ResetToken { get; set; } = string.Empty;

            // Used to track the user's account status (e.g., active, suspended)
            //[Required]
            //public bool IsActive { get; set; } = false;

            // A list of orders associated with the user
            public virtual ICollection<Order>? Orders { get; set; } = null;

            // A list of the user's shopping cart items (optional, based on business logic)
            public virtual ICollection<CartItem>? CartItems { get; set; } = null;
        }
    }
