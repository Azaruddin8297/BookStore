using System.ComponentModel.DataAnnotations;

namespace BookStore.Admin.Entity
{
    public class AdminEntity
    {
        [Key]
        public int AdminId { get; set; }
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
