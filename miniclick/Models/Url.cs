using System.ComponentModel.DataAnnotations;

namespace miniclick.Models
{
    public class Url
    {
        public int Id { get; set; }
        [Required]
        public string Uuid { get; set; } = null!;
        [Required]
        public string Link { get; set; } = null!;
    }
}
