﻿using WebApplication1.Data;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Linq;
using WebApplication1.DTO;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly SqlContext _db;

        public ProductsController(SqlContext db)
        {
            _db = db;
        }

        //GET: api/<ProductsController>
        [HttpGet]
        public IEnumerable<ProductsDto> Get()
        {
            return _db.Products
                .Join(_db.Images,
                    prod => prod.Pid,
                    img => img.Pid,
                    (p, i) => new ProductsDto { Pid = p.Pid, Name = p.Name, Description = p.Description, Active = p.Active, categories = p.categories, CreatedDate = p.CreatedDate, Price = p.Price, Sold = p.Sold, Author = p.Author, Image = i.EncodedString }).ToList();
        }
        [HttpGet("/IncludeImage")]
        public IEnumerable<Products> GetIncludeImage()
        {
            return _db.Products.Include(x => x.Images);

        }
        //[HttpGet("/IncludeImage/{id}")]
        //public IActionResult GetbyIdIncludeImage(int id)
        //{
        //  var item =_db.Products.Include(x => x.Images).FirstOrDefault(i => i.Pid == id);
        //    return Ok(item);

        //}
        [HttpGet("/IncludeImage/{id}")]
        public IActionResult GetbyIdIncludeImage(int id)
        {
            var item = _db.Products
                .Include(x => x.Images)
     
                .Where(i => i.Pid == id)
                .FirstOrDefault();

            return Ok(item);

        }

        // Samma resultat
        [HttpGet("{id}")]
        public Products Get(int id)
        {

            var result = _db.Products.Where(x => x.Pid == id).FirstOrDefault();

            return result;
        }

        // Samma resultat på dessa ^V

        [HttpGet("api/{id}")]
        public Products GetbyId(int id)
        {

            return _db.Products.Find(id);
        }

        [HttpGet("Name")]
        public IActionResult GetbyName(string name)

        {
            var prod = _db.Products.Where(p => p.Name == name).FirstOrDefault();
            if (prod == null)
            {
                return NotFound("");
            }
            return Ok(prod);
        }


        // POST api/<ProductsController>
        //[HttpPost]
        //public async Task<ActionResult<ProductsDto>> PostProduct(Products products)
        //{
        //    try
        //    {
        //        var _prod = _db.Products.Where(x => x.Pid == products.Pid).FirstOrDefault();
        //        if (_prod == null)
        //        {
        //            _db.Products.Add(products);
        //            await _db.SaveChangesAsync();

        //        }
        //    }
        //    catch
        //    {

        //    }
        //    return CreatedAtAction(nameof(PostProduct), new { id = products.Pid }, products);
        //}
        [HttpPost]
        public async Task<ActionResult<ProductsDto>> PostProduct(CreateProductDto productDto)
        {
            try
            {
                // Skapa en ny produkt baserat på DTO
                var newProduct = new Products
                {
                    Name = productDto.Name,
                    Description = productDto.Description,
                    categories = productDto.categories,
                    Author = productDto.Author,
                    Price = productDto.Price,
                    Active = productDto.Active,
                    Sold = productDto.Sold
                    // Sätt inte Image här eftersom det inte finns i DTO:n
                };

                // Lägg till den nya produkten i databasen
                _db.Products.Add(newProduct);
                await _db.SaveChangesAsync();
                var defaultImageString = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEBLAEsAAD/4QBWRXhpZgAATU0AKgAAAAgABAEaAAUAAAABAAAAPgEbAAUAAAABAAAARgEoAAMAAAABAAIAAAITAAMAAAABAAEAAAAAAAAAAAEsAAAAAQAAASwAAAAB/+0ALFBob3Rvc2hvcCAzLjAAOEJJTQQEAAAAAAAPHAFaAAMbJUccAQAAAgAEAP/hDIFodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvADw/eHBhY2tldCBiZWdpbj0n77u/JyBpZD0nVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkJz8+Cjx4OnhtcG1ldGEgeG1sbnM6eD0nYWRvYmU6bnM6bWV0YS8nIHg6eG1wdGs9J0ltYWdlOjpFeGlmVG9vbCAxMS44OCc+CjxyZGY6UkRGIHhtbG5zOnJkZj0naHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyc+CgogPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9JycKICB4bWxuczp0aWZmPSdodHRwOi8vbnMuYWRvYmUuY29tL3RpZmYvMS4wLyc+CiAgPHRpZmY6UmVzb2x1dGlvblVuaXQ+MjwvdGlmZjpSZXNvbHV0aW9uVW5pdD4KICA8dGlmZjpYUmVzb2x1dGlvbj4zMDAvMTwvdGlmZjpYUmVzb2x1dGlvbj4KICA8dGlmZjpZUmVzb2x1dGlvbj4zMDAvMTwvdGlmZjpZUmVzb2x1dGlvbj4KIDwvcmRmOkRlc2NyaXB0aW9uPgoKIDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PScnCiAgeG1sbnM6eG1wTU09J2h0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8nPgogIDx4bXBNTTpEb2N1bWVudElEPmFkb2JlOmRvY2lkOnN0b2NrOjM3MWE4YTRhLTJhNmYtNGQyNC1hZGU4LTViY2U1ZDI1ZjM3ZTwveG1wTU06RG9jdW1lbnRJRD4KICA8eG1wTU06SW5zdGFuY2VJRD54bXAuaWlkOjZjZDY5MmE2LTFhOWYtNGNmNS1iMzgxLWViYjYzODAyNmEzZDwveG1wTU06SW5zdGFuY2VJRD4KIDwvcmRmOkRlc2NyaXB0aW9uPgo8L3JkZjpSREY+CjwveDp4bXBtZXRhPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAo8P3hwYWNrZXQgZW5kPSd3Jz8+/9sAQwAFAwQEBAMFBAQEBQUFBgcMCAcHBwcPCwsJDBEPEhIRDxERExYcFxMUGhURERghGBodHR8fHxMXIiQiHiQcHh8e/9sAQwEFBQUHBgcOCAgOHhQRFB4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4e/8AAEQgBaAHgAwERAAIRAQMRAf/EABwAAQACAwEBAQAAAAAAAAAAAAABCAQFBgcDAv/EAE4QAAEDAgIEBw0FBQcBCQAAAAABAgMEBQYRByExQQgSE1FhdJEXIjI1N1NVcYGUsrPRFCNC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"; // Sätt din default bildsträng här
                var newImage = new Image
                {
                    Pid = newProduct.Pid, // Sätt produktens ID
                    EncodedString = defaultImageString,
                    ProductsPid = newProduct.Pid
                };

                // Lägg till den nya bilden i databasen
                _db.Images.Add(newImage);
                await _db.SaveChangesAsync();
                // Returnera CreatedAtAction med den nya produkten
                return CreatedAtAction(nameof(PostProduct), new { id = newProduct.Pid }, newProduct);
            }
            catch (Exception ex)
            {
                // Logga fel här om det är relevant för ditt program
                Console.WriteLine($"Exception: {ex.Message}");
                // Returnera ett BadRequest-resultat om något går fel
                return BadRequest();
            }
        }

        //PUT api/<ProductsController>/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, NewProductDto updatedProductDto)
        {
            try
            {
                // Hämta den befintliga produkten från databasen
                var existingProduct = await _db.Products.FindAsync(id);

                // Om produkten inte finns, returnera NotFound
                if (existingProduct == null)
                {
                    return NotFound();
                }

                // Uppdatera egenskaperna för den befintliga produkten baserat på DTO
                existingProduct.Name = updatedProductDto.Name;
                existingProduct.Description = updatedProductDto.Description;
                existingProduct.Price = updatedProductDto.Price;
               
                // Uppdatera andra egenskaper efter behov

                // Uppdatera databasen
                _db.Products.Update(existingProduct);
                await _db.SaveChangesAsync();

                // Returnera NoContent om allt går bra
                return NoContent();
            }
            catch (Exception ex)
            {
                // Logga fel här om det är relevant för ditt program
                Console.WriteLine($"Exception: {ex.Message}");
                // Returnera ett BadRequest-resultat om något går fel
                return BadRequest();
            }
        }



        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

     


        [HttpGet("GetCartById{cartId}")]
        public IActionResult GetCart(int cartId)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
              
            };

            var json = JsonSerializer.Serialize(_db.Carts
                .Include(c => c.CartItems)
                .FirstOrDefault(c => c.CartId == cartId), options);

            return Ok(json);
        }


        [HttpPost("cart")]
        public async Task<ActionResult<CartDto>> PostCart(CartItemDto cartDto)
        {
            Cart newCart = null;
            CartDto newCartDto = null;

            try
            {
                newCart = new Cart
                {
                    CreatedDate = DateTime.Now,
                    CartItems = new List<CartItem>()
                };

                {
                    var product = _db.Products.Find(cartDto.ProductId);

                    if (product != null)
                    {
                        var cartItem = new CartItem
                        {
                            ProductId = product.Pid,
                            Quantity = cartDto.Quantity,
                            Product = product
                        };

                        newCart.CartItems.Add(cartItem);
                    }

                    _db.Carts.Add(newCart);
                    _db.SaveChanges();

                    newCartDto = new CartDto
                    {
                        //CartId = newCart.CartId,
                        CartItems = newCart.CartItems.Select(ci => new CartItemDto
                        {
                            ProductId = ci.ProductId,
                            Quantity = ci.Quantity
                        }).ToList()
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return BadRequest();
            }

            return CreatedAtAction(nameof(PostCart), new { id = newCart.CartId }, newCartDto);
        }
        [HttpPut("{id}/image")]
        public async Task<IActionResult> UpdateProductImage(int id, [FromBody] string newEncodedImage)
        {
            try
            {
                var productImage = await _db.Images.FirstOrDefaultAsync(img => img.Pid == id);

                if (productImage == null)
                {
                    return NotFound($"No image found for product with ID {id}");
                }

                productImage.EncodedString = newEncodedImage;

                _db.Images.Update(productImage);
                await _db.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return StatusCode(500, "An error occurred while updating the image");
            }
        }


        [HttpGet("GetCartById/{cartId}")]
        public IActionResult GetCartById(int cartId)
        {
            try
            {
                var cart = _db.Carts
                    .Include(c => c.CartItems)
                        .ThenInclude(ci => ci.Product) // Inkludera produkterna för varje varukorgsobjekt
                    .FirstOrDefault(c => c.CartId == cartId);

                if (cart == null)
                {
                    return NotFound($"Cart with ID {cartId} not found");
                }

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true // Detta är valfritt, men gör JSON-resultatet mer läsbart
                };

                var json = JsonSerializer.Serialize(cart, options);

                return Ok(json);
            }
            catch (Exception ex)
            {
                // Logga fel här om det är relevant för ditt program
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("sold")]
        public IEnumerable<ProductsDto> GetSoldProducts()
        {
            var soldProducts = _db.Products
                .Where(p => p.Sold)
                .Select(p => new ProductsDto
                {
                    Pid = p.Pid,
                    CreatedDate = p.CreatedDate,
                    Name = p.Name,
                    Description = p.Description,
                    categories = p.categories,
                    Author = p.Author,
                    Price = p.Price,
                    Active = p.Active,
                    Sold = p.Sold
                    // Lägg inte till Images här eftersom vi inte behöver dem för denna förfrågan
                })
                .ToList();

            return soldProducts;
        }



    }

}


