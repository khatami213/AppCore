using DatabaseAccessLayer.EFCore.DBContexts;
using Domain.DTO.Security.Permisions;
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
    public class PermisionRepository : GenericRepository<PermisionDomain>, IPermisionDomain
    {
        private readonly ApplicationContext _context;

        public PermisionRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> AddRange(IEnumerable<PermisionDomain> permisionDomain)
        {
            if (permisionDomain == null)
                return false;

            await _context.Permisions.AddRangeAsync(permisionDomain);
            return true;
        }

        public async Task<PermisionDTO> GetAllPermisionsDTO()
        {
            var permisionDTO = new PermisionDTO();
            var permisions = await _context.Permisions.Select(r => new PermisionInfoDTO()
            {
                Id = r.Id,
                IsSelected = false,
                Title = r.Title,
                Value = r.Value
            }).ToListAsync();

            permisionDTO.Permisions = permisions;

            return permisionDTO;
        }
    }
}
