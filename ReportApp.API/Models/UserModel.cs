
using ReportApp.Shared;

namespace ReportApp.API.Models
{
    //user Model and database connection
    public class UserModel : IUserModel
    {
        private readonly AppDbContext _appDbContext;

        public UserModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<User> AddUser(User user)
        {
            var result = await _appDbContext.Users.AddAsync(user);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAll()
        {
            var foundDos = _appDbContext.Users.OrderByDescending(d => d.UserId).ToList();

            foreach (var Users in foundDos)
            {
                _appDbContext.Users.Remove(Users);
            }

            await _appDbContext.SaveChangesAsync();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _appDbContext.Users;
        }

        public User GetUserById(int userId)
        {
            return _appDbContext.Users.FirstOrDefault(c => c.UserId == userId);
        }
    }
}
