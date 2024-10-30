using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        // En varukorg kan ha flera produkter, så använd en ICollection
        public ICollection<CartItem> CartItems { get; set; }
    }

}
