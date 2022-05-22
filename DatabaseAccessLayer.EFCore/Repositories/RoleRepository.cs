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

        public async Task<IEnumerable<RoleDTO>> GetAllDTO()
        {
            var roles = await _context.Roles.ToListAsync();

            var res = new List<RoleDTO>();

            foreach (var role in roles)
            {
                res.Add(new RoleDTO() { Name = role.Name, Id = role.Id });
            }

            return res;
        }
    }
}
