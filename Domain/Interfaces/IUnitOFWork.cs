﻿using System;
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
        IUserDomain _user { get; set; }
        IPermisionDomain _permision { get; set; }
        IRolePermisionDomain _rolePermision { get; set; }
        IUserRoleDomain _userRole { get; set; }
        IPaymentDocumentDomain _paymentDocument { get; set; }

        void Complete();
    }
}
