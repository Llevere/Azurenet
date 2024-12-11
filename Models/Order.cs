using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Azurenet.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        // Foreign key for User
        [ForeignKey("UserId")]
        public int UserId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string ShippingAddress { get; set; }

        // Additional order details can be added (e.g., shipping method, payment method)

        public virtual User? User { get; set; }  // Navigation property

        // Navigation property for OrderItems (CartItems)
        public virtual ICollection<CartItem>? CartItems { get; set; }
    }

}
