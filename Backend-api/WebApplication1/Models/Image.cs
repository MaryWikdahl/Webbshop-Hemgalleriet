using System.ComponentModel.DataAnnotations;
namespace WebApplication1.Models
{
    public class Image
    {
        [Key]
        public int Iid { get; set; }

        public int Pid { get; set; }
        public int ProductsPid { get; set; }

        [Required]
        public string EncodedString { get; set; }
    }
}
