using System.ComponentModel.DataAnnotations;
using static WebApplication3.Controllers.BookController;
using System.Text.Json.Serialization;

namespace WebApplication3.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public string Publisher { get; set; }
        public decimal Price { get; set; }
    }
}
