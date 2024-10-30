using System.ComponentModel.DataAnnotations;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.DTO

{
    public class ProductsDto: Products
    {
      public string Image { get; set; }

    }
}
