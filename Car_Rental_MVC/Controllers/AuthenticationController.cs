using Car_Rental_MVC.Models;
using Car_Rental_MVC.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Car_Rental_MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserRepository _userRepository;
        public AuthenticationController(IUserRepository userRepository)
        {
            _userRepository = userRepository;   
        }
        [HttpGet("Login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            return View();
        }
        [HttpGet("Register")]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }


        [HttpPost("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password, string returnUrl)
        {
            if (_userRepository.UserExists(email, password))
            {
                var claims = new List<Claim>();
                claims.Add(new Claim("Email", email));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, email));
                claims.Add(new Claim(ClaimTypes.Name, email));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);

                if (string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect("/");
                }
                return LocalRedirect(returnUrl);
            }
            else
            {
                ModelState.AddModelError("LoginError", "Please check the information you entered and try again.");
                return View();
            }
            
        }

        [HttpPost("Register")]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterModel userInfo, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_userRepository.EmailAlreadyExists(userInfo.Email))
                {
                    ModelState.AddModelError("RegisterEmailError", "The Email already exists.");
                    return View("Register");
                }
                _userRepository.Add(userInfo);

                if (string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect("/");
                }

                return LocalRedirect(returnUrl);
            }

            return View(userInfo);
        }
    }
}
