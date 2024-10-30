using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        // Navigationsegenskaper för att koppla till produktmodellen
        public Products Product { get; set; }

        // Navigationsegenskaper för att koppla till varukorgsmodellen
        public Cart Cart { get; set; }
    }

}
