using System.Text.Json.Serialization;

namespace WebApplication1.DTO
{
    // DTO för CartItem
    public class CartItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    // DTO för Cart
    public class CartDto
    {
        public int CartId { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }

}
