using System;
using System.Collections.Generic;

namespace WebApplication2_Databasefirst.Models;

public partial class Image
{
    public int Id { get; set; }

    public byte[] ImageData { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
