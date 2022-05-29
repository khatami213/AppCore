using DatabaseAccessLayer.EFCore.DBContexts;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLayer.EFCore.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public IStudentDomain _student { get; set; }
        public ICourseDomain _course { get; set; }
        public IRoleDomain _role { get; set; }
        public IUserDomain _user { get; set; }
        public IPermisionDomain _permision { get; set; }
        public IRolePermisionDomain _rolePermision { get; set; }

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            _student = new StudentRepository(_context);
            _course = new CourseRepository(_context);
            _role = new RoleRepository(_context);
            _user = new UserRepository(_context);
            _permision = new PermisionRepository(_context);
            _rolePermision = new RolePermisionRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
            Dispose();
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }
}
