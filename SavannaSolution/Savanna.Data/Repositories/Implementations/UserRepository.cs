using Microsoft.EntityFrameworkCore;
using Savanna.Data;
using Savanna.Data.Data;
using Savanna.Data.Repositories.Interfaces;
using System.Collections.Generic;

namespace Savanna.Data.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly IApplicationDbContext _context;

        public UserRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.UserId == id);
            return user;
        }

        public async Task<User> GetUserByUsernameOrEmail(string username)
        {
            var user = _context.User.FirstOrDefault(user => user.UserName.Equals(username) || user.Email.Equals(username));
            return user;
        }

        public async Task<User> GetUserByUsernameAndEmail(string username, string email)
        {
            var user = _context.User.FirstOrDefault(u => u.UserName.Equals(username) && u.Email.Equals(email));
            return user;
        }

        public void Update(User user)
        {
            _context.Update(user);
        }

        public void Save(User user)
        {
            _context.SaveChanges();
        }

        public void Insert(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
        }
    }
}
