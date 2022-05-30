using DatabaseAccessLayer.EFCore.DBContexts;
using Domain.DTO.Security.Permisions;
using Domain.DTO.Security.RolePermision;
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
    public class RolePermisionRepository : GenericRepository<RolePermisionDomain>, IRolePermisionDomain
    {
        private readonly ApplicationContext _context;

        public RolePermisionRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<long>> GetAllPermisionsIdByRoleId(long roleId)
        {
            return await _context.RolePermisions.Where(r => r.RoleId == roleId).Select(r => r.PermisionId).ToListAsync();
        }

        public async Task<bool> DeletePermisionsByRoleId(long roleId)
        {
            var rolePermisions = await _context.RolePermisions.Where(r => r.RoleId == roleId).ToListAsync();
            _context.RolePermisions.RemoveRange(rolePermisions);

            return true;
        }

        public async Task<bool> AddRangeRolePermisionInfoDTO(List<RolePermisionDTO> rolePermisions)
        {
            await _context.RolePermisions.AddRangeAsync(rolePermisions.Select(r => new RolePermisionDomain()
            {
                PermisionId = r.PermisionId,
                RoleId = r.RoleId
            }));

            return true;
        }
    }
}
