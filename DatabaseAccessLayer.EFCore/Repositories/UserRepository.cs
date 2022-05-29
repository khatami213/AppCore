using DatabaseAccessLayer.EFCore.DBContexts;
using Domain.DTO.Account.Login;
using Domain.DTO.Account.Register;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLayer.EFCore.Repositories
{
    public class UserRepository : GenericRepository<UserDomain>, IUserDomain
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CheckPassword(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(r => r.Username == username);
            if (user == null)
                return false;
            if (user.Password == password)
                return true;

            return false;
        }

        public async Task<LoginDTO> GetByUsername(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(r => r.Username == username);
            if (user == null)
                return null;

            return new LoginDTO()
            {
                Username = username,
                Password = user.Password
            };

        }

        public async Task<bool> IsDuplicateByUsername(string username, long id)
        {
            return await _context.Users.AnyAsync(r => r.Username == username && r.Id != id);
        }

        public async Task<bool> IsDuplicateByUsernameAndUserType(string username, int userType, long id)
        {
            return await _context.Users.AnyAsync(r => r.Username == username && r.UserType == userType && r.Id != id);
        }

        public async Task<bool> RegisterUserDTO(RegisterDTO registerDTO)
        {
            if (registerDTO == null)
                return false;
            var newUser = new UserDomain()
            {
                Email = registerDTO.Email,
                Username = registerDTO.Username,
                Password = registerDTO.Password,
                UserType = registerDTO.UserType,
                IsActive = false,
                IsAdmin = false,
                CreatedOn = DateTime.Now
            };

            await _context.Users.AddAsync(newUser);
            return true;
        }
    }
}
