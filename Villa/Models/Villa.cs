using System.ComponentModel.DataAnnotations;

namespace Villa.Models
{
    public class Villa
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Location { get; set; }
        [Required]

        public string Price { get; set; }
       
        public string? PhotoUrl { get; set; }
    }
}
