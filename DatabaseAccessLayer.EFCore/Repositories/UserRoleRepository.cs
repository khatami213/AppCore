using DatabaseAccessLayer.EFCore.DBContexts;
using Domain.DTO.Security.UserRoles;
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
    public class UserRoleRepository : GenericRepository<UserRoleDomain>, IUserRoleDomain
    {
        private readonly ApplicationContext _context;

        public UserRoleRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> AddUserRoleDTO(UserRolesDTO userRolesDTO)
        {
            var userID = userRolesDTO.UserId;
            var userRoles = userRolesDTO.Roles.Select(r => new UserRoleDomain()
            {
                RoleId = r.RoleID,
                UserId = userID
            }).ToList();

            await _context.UserRoles.AddRangeAsync(userRoles);

            return true;

        }

        public async Task<bool> DeleteByUserId(long userId)
        {
            var userRoles = await _context.UserRoles.Where(r => r.UserId == userId).ToListAsync();

            _context.RemoveRange(userRoles);

            return true;
        }

        public async Task<List<long>> GetRolesByUserID(long userId)
        {
            return await _context.UserRoles.Where(r => r.UserId == userId).Select(r => r.RoleId).ToListAsync();
        }
    }
}
