using Azurenet.Products;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Azurenet.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        public int UserId { get; set; }  // Foreign key

        // Foreign key for Order
        [ForeignKey("OrderId")]
        public int OrderId { get; set; }

        // Foreign key for Product (assuming Product is another model in your application)
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public virtual User? User { get; set; }  // Navigation property

        // Navigation property for Order
        public virtual Order? Order { get; set; }

        // Navigation property for Product (assuming a Product model exists)
        public virtual Product? Product { get; set; }
    }

}
