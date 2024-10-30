using System.ComponentModel.DataAnnotations;
using WebApplication1.Data;
namespace WebApplication1.Models

{
    public class Products
    {
        [Key]
        public int Pid { get; set; }
  
      public DateTime CreatedDate { get; set; }
      public string Name { get; set; }

      public string Description { get; set; }

      public Categories categories { get; set; }

      public string Author { get; set; }

        public decimal Price { get; set; }

      public bool Active { get; set; }

      public bool Sold { get; set; }

      public ICollection<Image> Images { get; set; }

    }
}
