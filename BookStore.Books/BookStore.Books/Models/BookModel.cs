using System.ComponentModel.DataAnnotations;

namespace BookStore.Books.Models
{
    public class BookModel
    { 
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string Description { get; set; }
        public float Ratings { get; set; }
        public int Reviews { get; set; }
        public float DiscountedPrice { get; set; }
        public float OriginalPrice { get; set; }
        public int Quantity { get; set; }
    }
}
