using System.Text.Json.Serialization;
using WebApplication2_Databasefirst.Models;

namespace WebApplication2_Databasefirst.DTO
{
    public class ProductDto
    {
        public ProductDto(int id, DateTime createdDate, string name, string description, string author, decimal price, bool active, bool sold, int categoriesId, int imagesId, Category categories, Image images)
        {
            Id = id;
            CreatedDate = createdDate;
            Name = name;
            Description = description;
            Author = author;
            Price = price;
            Active = active;
            Sold = sold;
            CategoriesId = categoriesId;
            ImagesId = imagesId;
            Categories = categories;
            Images = images;
        }

        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Author { get; set; } = null!;

        public decimal Price { get; set; }

        public bool Active { get; set; }

        public bool Sold { get; set; }

        public int CategoriesId { get; set; }

        public int ImagesId { get; set; }
        [JsonIgnore]
        public virtual Category Categories { get; set; } = null!;
        [JsonIgnore]
        public virtual Image Images { get; set; } = null!;
    }
}
