using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace URLShortenerService.Models
{
    /// <summary>
    /// İsimlendirme URL,
    /// ShortenedUrl
    /// </summary>
    public class URL
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string OriginalUrl { get; set; }

        [Required]
        [MaxLength(6)]
        ///ShortenedUrl
        public string ShortUrl { get; set; }

        [Required]
        public string Domain { get; set; }

        [MaxLength(100)]
        /// string?
        public string CustomShortUrl { get; set; }
    }

}
