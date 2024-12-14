using System.ComponentModel.DataAnnotations;
using Azurenet.Models;

namespace Azurenet.Products
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string? ProductName { get; set; } = string.Empty;
        public string? ProductDescription { get; set; } = string.Empty;
        public Categories ProductCategory { get; set; } = Categories.RandomCategory;

    }
}