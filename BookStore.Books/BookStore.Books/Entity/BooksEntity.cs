using System.ComponentModel.DataAnnotations;

namespace BookStore.Books.Entity
{
    public class BooksEntity
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        public string BookName { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        public string Description { get; set; }
        public float Ratings { get; set; }
        public int Reviews { get; set; }
        [Required]
        public float DiscountedPrice { get; set; }
        [Required]
        public float OriginalPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
