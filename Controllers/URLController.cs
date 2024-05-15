// URLController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using URLShortenerService.Models;

namespace URLShortenerService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class URLController : ControllerBase
    {
        private readonly AppDbContext _context;

        public URLController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult<string> ShortenURL([FromBody] ShortenURLRequestModel model)
        {
            if (ModelState.IsValid)
            {
                if (Uri.TryCreate(model.OriginalUrl, UriKind.Absolute, out Uri validatedUri))
                {
                    string domain = validatedUri.GetLeftPart(UriPartial.Authority);
                    string path = validatedUri.PathAndQuery;

                    string shortUrl = model.CustomShortUrl; 
                    if (string.IsNullOrEmpty(shortUrl))
                    {
                        shortUrl = GenerateShortUrl(); 
                    }
                    if (_context.URLs.Any(u => u.CustomShortUrl == shortUrl))
                    {
                        return BadRequest("Custom short URL is already in use.");
                    }

                    var url = new URL { OriginalUrl = model.OriginalUrl, ShortUrl = shortUrl, Domain = domain, CustomShortUrl = model.CustomShortUrl };
                    _context.URLs.Add(url);
                    _context.SaveChanges();

                    string fullShortenedUrl = $"{domain}/{shortUrl}/";
                    return Created(fullShortenedUrl, fullShortenedUrl); 
                }
                return BadRequest("Invalid URL format");
            }
            return BadRequest(ModelState);
        }

        [HttpGet("{shortUrl}")]
        public IActionResult Redirect(string shortUrl)
        {
            var url = _context.URLs.FirstOrDefault(u => u.ShortUrl == shortUrl);
            if (url != null)
            {
                return Ok(new { OriginalUrl = url.OriginalUrl });
            }
            else
            {
                return NotFound();
            }
        }

        private string GenerateShortUrl()
        {
            // Generate a random alphanumeric short URL
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var shortUrl = new string(Enumerable.Repeat(chars, 6)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            return shortUrl;
        }
    }
}

