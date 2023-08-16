using Microsoft.EntityFrameworkCore;
using PyggApi.Interfaces.Members;
using PyggApi.Models;

namespace PyggApi.Business.Members
{
    public class UserBusiness : IUsers 
    {
        private readonly ApiDbContext _context;
        public UserBusiness(ApiDbContext context)
        {
            _context = context;
        }

        public  async Task<List<User>> AddUser(User user)
        {
            var userExists = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            if (userExists == null)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return await _context.Users.ToListAsync();
             
            }
            else
            {
                throw new Exception("User with the same email already exists");
            }
        }

        public async Task<User> LoginUser(User user)
        {
            var loggedInUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);

            return loggedInUser;
        }

        //public async Task<List<User>> LoginUser(User user)
        //{
        //   await _context.select * from users where email = @email and password = @password


        //    return await _context.Users.ToListAsync();
        //}
    }
}
