using TaskFlow.Models;

namespace TaskFlow.Services
{
    public interface IUserService
    {
        Task<bool> Register(string username, string firstName, string lastName, string password);
        Task<User> Login(string username, string password);
        Task<bool> Delete(int id);
        User FindById(int id);
        List<User> GetAll();
    }
}
