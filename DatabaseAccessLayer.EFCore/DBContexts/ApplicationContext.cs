using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLayer.EFCore.DBContexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<StudentDomain> Students { get; set; }
        public DbSet<CourseDomain> Courses { get; set; }
        public DbSet<RoleDomain> Roles { get; set; }
        public DbSet<UserDomain> Users { get; set; }
        public DbSet<PermisionDomain> Permisions { get; set; }
        public DbSet<RolePermisionDomain> RolePermisions { get; set; }
    }
}
