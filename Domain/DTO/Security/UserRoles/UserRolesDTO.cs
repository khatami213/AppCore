using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Security.UserRoles
{
    public class UserRolesDTO
    {
        public UserRolesDTO()
        {
            Roles = new List<UserRoleInfoDTO>();
        }
        public long UserId { get; set; }
        public List<UserRoleInfoDTO> Roles { get; set; }
    }

    public class UserRoleInfoDTO
    {
        public long RoleID { get; set; }
        public string Title { get; set; }
        public bool IsSelected { get; set; }
    }
}
