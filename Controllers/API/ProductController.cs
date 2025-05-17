using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task.ApplicationContext;
using Task.Models;

namespace Task.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Protect endpoint
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ProductController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpPost("add")]
        public IActionResult AddProduct([FromBody] ProductMaster product)
        {
            var employeeId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // ✅ Get token from Authorization header
            var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer "))
                return Unauthorized("Missing or invalid token.");

            var token = authHeader.Substring("Bearer ".Length).Trim();

            // ✅ Check token exists in TokenMaster
            var tokenExists = _context.TokenMaster.Any(t => t.EmployeeID == employeeId && t.Token == token);
            if (!tokenExists)
                return Unauthorized("Token not valid or has been revoked.");

            // ✅ Save Product
            product.AddedBy = employeeId;
            _context.ProductMaster.Add(product);
            _context.SaveChanges();

            return Ok("Product added.");
        }

        [HttpGet("all")]
        public IActionResult GetProductsWithEmployee()
        {
            var products = (from p in _context.ProductMaster
                           join e in _context.UserMaster
                           on p.AddedBy equals e.EmployeeID
                           select new 
                           {
                               p.GroupID,
                               p.Name,
                               p.MRP,
                               p.Price,
                               AddedBy = e.Name
                           }).ToList();

            return Ok(products);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.ProductMaster.FirstOrDefault(p => p.GroupID == id);
            if (product == null)
                return NotFound("Product not found.");

            _context.ProductMaster.Remove(product);
            _context.SaveChanges();

            return Ok("Product deleted successfully.");
        }


        [HttpPut("edit/{id}")]
        public IActionResult EditProduct(int id, [FromBody] ProductMaster updatedProduct)
        {
            var employeeId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer "))
                return Unauthorized("Missing or invalid token.");

            var token = authHeader.Substring("Bearer ".Length).Trim();
            var tokenExists = _context.TokenMaster.Any(t => t.EmployeeID == employeeId && t.Token == token);
            if (!tokenExists)
                return Unauthorized("Token not valid or has been revoked.");

            var product = _context.ProductMaster.FirstOrDefault(p => p.GroupID == id);
            if (product == null)
                return NotFound("Product not found.");

            // Update fields
            product.Name = updatedProduct.Name;
            product.MRP = updatedProduct.MRP;
            product.Price = updatedProduct.Price;

            _context.SaveChanges();

            return Ok("Product updated successfully.");
        }


    }
}
