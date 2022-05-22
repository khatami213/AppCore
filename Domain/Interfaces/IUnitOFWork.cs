using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentDomain _student { get; set; }
        ICourseDomain _course { get; set; }
        IRoleDomain _role { get; set; }
        void Complete();
    }
}
