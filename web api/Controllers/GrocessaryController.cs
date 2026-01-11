using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_api.Models;

namespace web_api.Controllers
{
    [Route("api/Grocessary")]
    [ApiController]
    public class GrocessaryController : ControllerBase
    {
        private readonly GroceryDbContext _context;

        public GrocessaryController(GroceryDbContext groceryDbContext)
        {
            _context = groceryDbContext;
        }

        [HttpPost]
        [Route("addproduct")]
        public IActionResult AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok();

        }

        [HttpGet]
        [Route("GetAllProduct")]
        public IActionResult GetAllProduct()
        {
            var products = _context.Products;
            return Ok(products);

        }

        [HttpGet]
        [Route("GetProductById/{id:int}")]
        public IActionResult GetProductById(int id)
        {
            try
            {
                var products = _context.Products.Find(id);
                if (products == null)
                    return NotFound("product not found");
                return Ok(products);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server Error: {ex.GetType().Name} -{ex.Message}");
            }
        }

        [HttpPost]
        [Route("UpadateProduct")]
        public IActionResult UpadateProduct([FromBody] Product updateproduct)
        {

            var product = _context.Products.Find(updateproduct.Id);
            if (product == null)
                return NotFound();

            //product.Id = updateproduct.Id;
            product.Name = updateproduct.Name;
            product.Price = updateproduct.Price;
            product.Shelflife = updateproduct.Shelflife;

            _context.SaveChanges();
            return Ok(product);

        }

        [HttpDelete]
        [Route("delete/{id:int}")]

        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return Ok(new { message = "Product Deleted Successfully" });

        }

    }
}
