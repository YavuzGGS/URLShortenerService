using System.ComponentModel.DataAnnotations;


namespace URLShortenerService.Models
{
    public class URL
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string OriginalUrl { get; set; }

        [Required]
        [MaxLength(6)]
        public string ShortUrl { get; set; }

        [Required]
        public string Domain { get; set; } 
    }

}
