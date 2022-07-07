using Car_Rental_MVC.Models;

namespace Car_Rental_MVC.Repositories
{
    public interface IUserRepository
    {
        bool UserExists(string email, string password);
        UserModel GetUserByEmail(string email);
        UserModel GetUserById(int userId);
        bool EmailAlreadyExists(string email);
        void Add(UserModel user);
    }
}
