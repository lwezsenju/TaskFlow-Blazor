using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using PassswortManagerAPI.Helpers;
using TaskFlow.Models;

namespace TaskFlow.Services
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext _ctx;
        public UserService(DatabaseContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<bool> Register(string username, string firstName, string lastName, string password)
        {
            var regex = new Regex("^[a-zA-Z0-9_.]+$");
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || !regex.IsMatch(username))
            {
                return false;
            }
            try
            {
                var existingUser = await _ctx.Users
                    .FirstOrDefaultAsync(u => u.UserName.ToLower() == username.ToLower());

                if (existingUser != null)
                {
                    return false;
                }
                User user = new User();
                user.Salt = Hashing.CreateSalt(32);
                user.FirstName = firstName;
                user.LastName = lastName;
                user.Password = Hashing.CreatePasswordHash(password, user.Salt);
                user.Role = "User";
                user.UserName = username.ToLower();
                _ctx.Users.Add(user);
                _ctx.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<User> Login(string username, string password)
        {
            try
            {
                var FoundUser = await _ctx.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower());

                if (FoundUser == null || FoundUser.Password != Hashing.CreatePasswordHash(password, FoundUser.Salt))
                {
                    return null;
                }

                return FoundUser;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                User user = (await _ctx.Users.FindAsync(id));
                _ctx.Users.Remove(user);
                _ctx.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }

        public User FindById(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }


    }
}
