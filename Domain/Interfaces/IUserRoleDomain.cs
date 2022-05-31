using Domain.DTO.Security.UserRoles;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRoleDomain : IGenericDomain<UserRoleDomain>
    {
        Task<List<long>> GetRolesByUserID(long userId);
        Task<bool> DeleteByUserId(long userId);

        Task<bool> AddUserRoleDTO(UserRolesDTO userRolesDTO);
    }
}
