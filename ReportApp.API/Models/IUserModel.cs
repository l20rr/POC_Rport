using ReportApp.Shared;

namespace ReportApp.API.Models
{
    public interface IUserModel
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int userId);
        Task<User> AddUser(User user);
    }
}
