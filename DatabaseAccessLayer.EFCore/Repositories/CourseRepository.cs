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
    public class CourseRepository : GenericRepository<CourseDomain>, ICourseDomain
    {
        private readonly ApplicationContext _context;

        public CourseRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
