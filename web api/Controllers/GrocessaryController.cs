using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_api.Models;

namespace web_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GrocessaryController : ControllerBase
    {
        private readonly GroceryDbContext _context;

        public GrocessaryController(GroceryDbContext groceryDbContext)
        {
            _context = groceryDbContext;
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok();

        }


      


        [HttpGet]
        public IActionResult GetAllProduct()
        {
            var products = _context.Products;
            return Ok(products);

        }




        [HttpGet("{id}")]
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




        [HttpPut("{id}")]
        public IActionResult UpadateProduct(int id, [FromBody] Product updateproduct)
        {

            var product = _context.Products.Find(id);
            if (product == null)
                return NotFound();

            //product.Id = updateproduct.Id;
            product.Name = updateproduct.Name;
            product.Price = updateproduct.Price;
            product.Shelflife = updateproduct.Shelflife;

            _context.SaveChanges();
            return Ok(product);

        }



        [HttpDelete("{id}")]

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
