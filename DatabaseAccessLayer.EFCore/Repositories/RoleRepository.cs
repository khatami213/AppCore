using DatabaseAccessLayer.EFCore.DBContexts;
using Domain.DTO.Security.Roles;
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
    public class RoleRepository : GenericRepository<RoleDomain>, IRoleDomain
    {
        private readonly ApplicationContext _context;

        public RoleRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddRoleDTO(NewRoleDTO roleDTO)
        {
            var newRole = new RoleDomain()
            {
                Name = roleDTO.Name,
            };
            await _context.Roles.AddAsync(newRole);
        }

        public async Task<RoleDTO> GetAllDTO()
        {
            var roleDTO = new RoleDTO();

            roleDTO.RolesInfo.AddRange(await _context.Roles.Select(
                r => new RoleInfoDTO()
                {
                    Id = r.Id,
                    Name = r.Name
                }
                )
                .ToListAsync());

            return roleDTO;
        }

        public bool IsDuplicateByName(long id, string name)
        {
            return _context.Roles.Any(r => r.Name == name && r.Id != id);
        }

        public async Task UpdateRoleDTO(UpdateRoleDTO roleDTO)
        {
            var oldRole = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleDTO.Id);
            if (oldRole != null)
            {
                oldRole.Name = roleDTO.Name;
            }
        }
    }
}
