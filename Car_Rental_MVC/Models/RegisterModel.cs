using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Car_Rental_MVC.Models
{
    public class RegisterModel : UserModel
    {
        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "The Confirm Password field is required")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [MaxLength(50)]
        public string ConfirmPassword { get; set; }
    }
}
