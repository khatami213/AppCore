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
    public class StudentRepository : GenericRepository<StudentDomain>, IStudentDomain
    {
        private readonly ApplicationContext _context;
        public StudentRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
