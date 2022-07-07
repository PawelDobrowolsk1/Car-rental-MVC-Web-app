using Microsoft.EntityFrameworkCore;

namespace Car_Rental_MVC.Models
{
    public class CarRentalManagerContext : DbContext
    {
        public CarRentalManagerContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<CarModel> Cars { get; set; }   
        public DbSet<RentedCarInfo> RentInfo { get; set; }   
    }
}
