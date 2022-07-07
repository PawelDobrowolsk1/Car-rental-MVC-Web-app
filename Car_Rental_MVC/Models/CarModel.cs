using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Rental_MVC.Models
{
    [Table("Cars")]
    public class CarModel
    {
        [Key]
        public int CarId { get; set; }
        public string Category { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public double? Engine { get; set; }
        public int Horsepower { get; set; }
        public int Year { get; set; }
        public int Seats { get; set; }
        public int Doors { get; set; }
        public string? Fuel { get; set; }
        public string Transmisson { get; set; }
        public string? Description { get; set; }
        public bool Available { get; set; }
    }
}
