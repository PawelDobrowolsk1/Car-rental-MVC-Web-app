using Car_Rental_MVC.Models;

namespace Car_Rental_MVC.Repositories
{
    public interface ICarRepository
    {
        CarModel GetCarById(int carId);
        IQueryable<CarModel> GetAll();
        IQueryable<CarModel> GetAllAvailable();
        void RentCar(string email, int carId);
        IEnumerable<CarModel> RentedCarsByUser(string email);
        void GiveBackCar(string email, int carId); 
    }
}
