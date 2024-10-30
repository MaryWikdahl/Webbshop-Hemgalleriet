using WebApplication1.Data;

namespace WebApplication1.DTO
{
   
        public class CreateProductDto
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public Categories categories { get; set; }
            public string Author { get; set; }
            public decimal Price { get; set; }
            public bool Active { get; set; }
            public bool Sold { get; set; }
            // Lägg till fler egenskaper vid behov
        }
    
}
