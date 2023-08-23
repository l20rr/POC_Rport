using ReportApp.Shared;

namespace ReportApp.Services
{
	public interface IUserDataService
	{

        Task<IEnumerable<User>> GetAllUsers();

        Task<User> GetUserId(int userId);
        Task<User> AddUser(User user);
   
    }
}
