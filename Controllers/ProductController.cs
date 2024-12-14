using Azurenet.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Diagnostics;

namespace Azurenet.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ProductController(AppDbContext context, IConfiguration configuration) : ControllerBase
    {
        private readonly AppDbContext _context = context;
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")!;


        //Entity framework
        [HttpGet("get-products")]
        public async Task<IActionResult> GetProducts()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        //Entity framework
        [HttpGet("get-product/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Invalid product ID." });
            }

            Product? product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found." });
            }
            return Ok(product.ProductCategory.GetTypeCode());
        }


        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            
            if (string.IsNullOrWhiteSpace(product.ProductName))
            {
                return BadRequest(new { Message = "Product name cannot be empty." });
            }
            //Trim any leading spaces
            product.ProductName = product.ProductName.Trim();

            //var existingProduct = await (from products in _context.Products
            //                       where products.ProductId == product.ProductId
            //                       select products).FirstOrDefaultAsync();
            var existingProduct = await _context.Products
                                    .FirstOrDefaultAsync(p => p.ProductId == product.ProductId);



            if (existingProduct != null)
            {
                return BadRequest(new {Message = "Product already exists."});
            }
            _context.Products.Add(product);
            _context.SaveChanges();

            return Ok(product);
        }

        [HttpPut("update-product")]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            // Retrieve the product by ID
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { message = "Product not found." });
            }

            // Update the product name
            product.ProductName = "Test";

            // Save the changes to the database
            await _context.SaveChangesAsync();

            return Ok(new { message = "Product updated successfully.", product });
        }

        //[HttpGet("get-all-products")]
        //public async Task<IActionResult> GetAllProductsDapper()
        //{
        //    const string sql = "SELECT * FROM Products";

        //    using (var connection = new MySqlConnection(_connectionString))
        //    {
        //        await connection.OpenAsync();

        //        var products = await connection.QueryAsync<Product>(sql);

        //        return Ok(products);
        //    }
        //}

        //[HttpGet("get-product/{id}")]
        //public async Task<IActionResult> GetProductDapper(int id)
        //{
        //    const string sql = "SELECT * FROM Products WHERE ProductId = @ProductId";

        //    using (var connection = new MySqlConnection(_connectionString))
        //    {
        //        await connection.OpenAsync();

        //        // Pass the parameter to the query
        //        var product = await connection.QueryFirstOrDefaultAsync<Product>(sql, new { ProductId = id });

        //        if (product == null)
        //        {
        //            return NotFound(new { message = "Product not found." });
        //        }

        //        return Ok(product);
        //    }
        //}

        [HttpDelete("delete-products")]
        public async Task<IActionResult> DeleteProducts()
        {
            var allProducts = await _context.Products.ToListAsync();
            if (allProducts.Count == 0)
            {
                return NotFound(new { message = "No products found to delete." });
            }

            _context.Products.RemoveRange(allProducts);
            await _context.SaveChangesAsync();
            return Ok(new { message = "All products deleted successfully." });
        }

        [HttpDelete("delete-products/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { message = "Product not found." });
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Product deleted successfully." });
        }
    }
}
