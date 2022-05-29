using DatabaseAccessLayer.EFCore.DBContexts;
using Domain.Entities;
using Domain.Interfaces;
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
    }
}
