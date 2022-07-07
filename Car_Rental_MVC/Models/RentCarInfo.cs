using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Rental_MVC.Models
{
    [Table("RentedCarInfo")]
    public class RentedCarInfo
    {
        [Key]
        public int RentedId { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public bool IsGivenBack { get; set; }

    }
}
