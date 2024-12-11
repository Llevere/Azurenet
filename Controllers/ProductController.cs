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
        public async Task<IActionResult> GetEFCore()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        [HttpGet("get-all-products")]
        public async Task<IActionResult> GetAllProductsDapper()
        {
            const string sql = "SELECT * FROM Products WHERE ProductId = 3321";

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var products = await connection.QueryAsync<Product>(sql);
                return Ok(products);
            }
        }



        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            for(int i = 1000; i < 5000; i++)
            {
                product.ProductId = i;
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
            }

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
