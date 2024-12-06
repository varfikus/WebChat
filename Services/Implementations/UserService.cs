using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebChat.Models;
using WebChat.Services.Interfaces;

namespace WebChat.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ChatContext _context;

        public UserService(ChatContext context)
        {
            _context = context;
        }

        public async Task<User> AuthenticateUserAsync(string email, string password)
        {
            try
            {
                return await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }


        public async Task<User> AuthenticateAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return null; 
            }

            return user;
        }
        public async Task<User> GetUserAsync(string token)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Token == token);
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
