// URLController.cs
using System.ComponentModel.DataAnnotations;

namespace URLShortenerService.Models
{
    public class ShortenURLRequestModel
    {
        [Required]
        public string OriginalUrl { get; set; }

        public string CustomShortUrl { get; set; }
    }
}