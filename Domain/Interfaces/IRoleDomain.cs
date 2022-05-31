using Domain.DTO.Security.Roles;
using Domain.DTO.Security.UserRoles;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRoleDomain : IGenericDomain<RoleDomain>
    {
        Task<RoleDTO> GetAllDTO();
        bool IsDuplicateByName(long id, string name);
        Task AddRoleDTO(NewRoleDTO roleDTO);
        Task UpdateRoleDTO(UpdateRoleDTO roleDTO);
        Task<UserRolesDTO> GetAllRolesDTO();
    }
}
