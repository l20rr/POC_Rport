
using ReportApp.Shared;

namespace ReportApp.API.Models
{
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
