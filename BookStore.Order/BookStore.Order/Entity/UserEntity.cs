using System.ComponentModel.DataAnnotations;

namespace BookStore.Order.Entity
{
    public class UserEntity
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}