using System;
using System.Collections.Generic;

namespace WebApplication2_Databasefirst.Models;

public partial class Product
{
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

    public virtual Category Categories { get; set; } = null!;

    public virtual Image Images { get; set; } = null!;
}
