namespace WebApplication2_Databasefirst.DTO
{
    public class CreateProduktDto
    {
        public CreateProduktDto(int id, string name, string description, string author, decimal price, bool active, bool sold, int categoriesId, int imagesId)
        {
            Id = id;
            Name = name;
            Description = description;
            Author = author;
            Price = price;
            Active = active;
            Sold = sold;
            CategoriesId = categoriesId;
            ImagesId = imagesId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
        public bool Sold { get; set; }
        public int CategoriesId { get; set; }
        public int ImagesId { get; set; } 
     

    }
}
