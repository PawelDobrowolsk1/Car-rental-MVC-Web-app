using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Rental_MVC.Models
{
    [Table("Users")]
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "The First Name field is required")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "The Last Name field is required")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "The Email field is required")]
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; }

        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "The Password field is required")]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}
