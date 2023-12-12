using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Order
    {

        [Key] public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int BookId { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Price { get; set; }

    }
}
