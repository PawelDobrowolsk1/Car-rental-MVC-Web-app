using Car_Rental_MVC.Models;
using Car_Rental_MVC.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarRepository _carRepository;


        public HomeController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public IActionResult BackToPreviousPage(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl))
            {
                return Redirect("/");
            }
            return LocalRedirect(returnUrl);
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AllAvailableCars()
        {
            return View(_carRepository.GetAllAvailable());
        }

        [AllowAnonymous]
        public IActionResult Cars()
        {
            return View(_carRepository.GetAll());
        }

        public IActionResult Details(int carId, string returnUrl)
        {
            TempData["ReturnUrl"] = returnUrl;
            return View(_carRepository.GetCarById(carId));
        }

        [Authorize]
        public IActionResult RentCar(string email, int carId)
        {
            if (User.Identity.Name == email)
            {
                _carRepository.RentCar(email, carId);
            }
            else
            {
                return Redirect(nameof(Cars));
            }

            return Redirect("/");
        }

        [Authorize]
        public IActionResult RentedCars()
        {
            return View(_carRepository.RentedCarsByUser(User.Identity.Name));
        }

        [Authorize]
        public IActionResult GiveBackCar(string email, int carId)
        {
            if (User.Identity.Name == email)
            {
                _carRepository.GiveBackCar(email, carId);
            }
            else
            {
                return Redirect(nameof(RentedCars));
            }

            return Redirect(nameof(RentedCars));
        }

    }
}
