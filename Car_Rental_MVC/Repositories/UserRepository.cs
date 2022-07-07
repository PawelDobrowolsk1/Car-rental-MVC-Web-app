using Car_Rental_MVC.Models;

namespace Car_Rental_MVC.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CarRentalManagerContext _context;

        public UserRepository(CarRentalManagerContext context)
        {
            _context = context;
        }
        public bool UserExists(string email, string password)
            => _context.Users.Any(u => u.Email == email && u.Password == password);
        
        public UserModel GetUserByEmail(string email)
            => _context.Users.SingleOrDefault(u => u.Email == email);
        
        public UserModel GetUserById(int userId)
            => _context.Users.SingleOrDefault(u => u.UserId == userId);
        
        public bool EmailAlreadyExists(string email)
            => _context.Users.Any(u => u.Email == email);
        public void Add(UserModel user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }

        
    }
}
