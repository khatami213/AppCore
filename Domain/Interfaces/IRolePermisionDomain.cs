using Domain.DTO.Security.Permisions;
using Domain.DTO.Security.RolePermision;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRolePermisionDomain : IGenericDomain<RolePermisionDomain>
    {
        Task<List<long>> GetAllPermisionsIdByRoleId(long roleId);
        Task<bool> DeletePermisionsByRoleId(long roleId);

        Task<bool> AddRangeRolePermisionInfoDTO(List<RolePermisionDTO> rolePermisions);
    }
}
