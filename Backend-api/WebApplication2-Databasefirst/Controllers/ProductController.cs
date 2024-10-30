using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2_Databasefirst.Data;
using WebApplication2_Databasefirst.DTO;
using WebApplication2_Databasefirst.Models;

namespace WebApplication2_Databasefirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly SqlContext _new_db;

        public ProductController(SqlContext db)
        {
            _new_db = db;
        }
    

    [HttpGet("{id}")]
    public Product GetById(int id)
    {

        var result = _new_db.Products.Where(x => x.Id == id).FirstOrDefault();

        return result;
    }
   [HttpGet("/images/{id}")]
        public async Task<ActionResult<ProductDto>> GetByIdInclude(int id)
        {
            var result = _new_db.Products
                .Include(p => p.Categories)
                .Include(p => p.Images)
                .FirstOrDefault(x => x.Id == id);

            return Ok(new ProductDto(result.Id, result.CreatedDate, result.Name, result.Description, result.Author, result.Price, result.Active, result.Sold, result.CategoriesId, result.ImagesId, result.Categories, result.Images));
        }

        [HttpGet("Name")]
    public IActionResult GetbyName(string name)

    {
        var prod = _new_db.Products.Where(p => p.Name == name).FirstOrDefault();
        if (prod == null)
        {
            return NotFound("");
        }
        return Ok(prod);
    }
    [HttpGet("/IncludeImage")]
        public IEnumerable<Product> GetIncludeImage()
        {
            return _new_db.Products.Include(x => x.Images);

        }

        // POST api/<ProductsController>
        [HttpPost]
        //public async Task<ActionResult<ProductsincludeImageDto>> PostProduct(Product products)
        //{
        //    try
        //    {
        //        var _prod = _new_db.Products.Where(x => x.Id == products.Id).FirstOrDefault();
        //        if (_prod == null)
        //        {
        //            _new_db.Products.Add(products);
        //            await _new_db.SaveChangesAsync();

        //        }
        //    }
        //    catch
        //    {

        //    }
        //    return CreatedAtAction(nameof(PostProduct), new { id = products.Id }, products);
        //}



        public async Task<ActionResult<Product>> CreateProduct([FromBody] CreateProduktDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Invalid data");
            }

            var product = new Product
            {
                CreatedDate = DateTime.Now,
                Name = productDto.Name,
                Description = productDto.Description,
                Author = productDto.Author,
                Price = productDto.Price,
                Active = productDto.Active,
                Sold = productDto.Sold,
                CategoriesId = productDto.CategoriesId,
                ImagesId = productDto.ImagesId
            };

            _new_db.Products.Add(product);
            await _new_db.SaveChangesAsync();

            // Returnera den skapade produkten som svar på POST-förfrågan.
            return Ok(product);
        }

        public byte[] GetImageData(CreateImageDto imageDto)
        {
            // Läs in bildfilen som en byte-array
            byte[] imageData;

            try
            {
                imageData = imageDto.ImageData;
            }
            catch (Exception ex)
            {
                // Hantera eventuella fel här
                Console.WriteLine("Fel vid läsning av bildfil: " + ex.Message);
                return null;
            }

            return imageData;
        }
        [HttpPost("/Image")]
        public IActionResult CreateImage([FromBody] CreateImageDto imageDto)
        {
            // Skapa en Image-objekt från ImageDto
            var image = new Image
            {
                ImageData = imageDto.ImageData,
                // Om du vill inkludera Products, kan du göra det här.
                // Products = imageDto.Products
            };

            // Lägg till och spara bilden i din databas
            _new_db.Images.Add(image);
            _new_db.SaveChanges();

            // Returnera en lämplig respons, t.ex. ID för den sparade bilden
            return Ok(image.Id);
        }

        //PUT api/<ProductsController>/5

        [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<ProductsController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }

    }
}