using System.ComponentModel.DataAnnotations;
using WebApplication2_Databasefirst.Data;
using WebApplication2_Databasefirst.Models;

namespace WebApplication2_Databasefirst.DTO
{
    public class ProductsincludeImageDto: Product
    {
      public string Image { get; set; }

    }
}
