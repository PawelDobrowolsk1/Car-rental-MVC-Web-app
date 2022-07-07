using Car_Rental_MVC.Models;
using System.Linq;

namespace Car_Rental_MVC.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly CarRentalManagerContext _context;
        public CarRepository(CarRentalManagerContext context)
        {
            _context = context;
        }

        public CarModel GetCarById(int carId)
            => _context.Cars.SingleOrDefault(c => c.CarId == carId);

        public IQueryable<CarModel> GetAll()
            => _context.Cars.Select(c => c);

        public IQueryable<CarModel> GetAllAvailable()
            => _context.Cars.Where(x => x.Available);

        public void RentCar(string email, int carId)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == email);
            var car = _context.Cars.SingleOrDefault(c => c.CarId == carId);
            if (car != null)
            {
                car.Available = false;
            }

            var rentInfo = new RentedCarInfo()
            {
                UserId = user.UserId,
                CarId = car.CarId,
                IsGivenBack = false
            };

            _context.RentInfo.Add(rentInfo);
            _context.SaveChanges();
        }

        public IEnumerable<CarModel> RentedCarsByUser(string email)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == email);
            var carsId = _context.RentInfo.Where(u => u.UserId == user.UserId && u.IsGivenBack == false).Select(c => c.CarId).ToList();
            if (carsId.Any())
            {
                var car = new List<CarModel>();

                for (int i = 0; i < carsId.Count(); i++)
                {
                    var a = _context.Cars.Where(c => c.CarId == carsId[i]);

                    foreach (var item in a)
                    {
                        car.Add(new CarModel()
                        {
                            CarId = item.CarId,
                            Category = item.Category,
                            Make = item.Make,
                            Model = item.Model,
                            Engine = item.Engine,
                            Horsepower = item.Horsepower,
                            Year = item.Year,
                            Seats = item.Seats,
                            Doors = item.Doors,
                            Fuel = item.Fuel,
                            Transmisson = item.Transmisson,
                            Description = item.Description,
                            Available = item.Available
                        });
                    }
                }
                return car;
            }
            else
            {
                return null;
            }
        }

        public void GiveBackCar(string email, int carId)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == email);
            var car = _context.Cars.SingleOrDefault(c => c.CarId == carId);
            if (car != null)
            {
                car.Available = true;
            }

            var rentedInfo = _context.RentInfo.SingleOrDefault(x => x.UserId == user.UserId && x.CarId == car.CarId && x.IsGivenBack == false);
            if (rentedInfo != null)
            {
                rentedInfo.IsGivenBack = true;
            }

            _context.SaveChanges();
        }
    }
}
