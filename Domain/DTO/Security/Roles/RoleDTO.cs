using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Security.Roles
{
    public class RoleDTO
    {
        public RoleDTO()
        {
            Actions = new List<ActionItems>();
            RolesInfo = new List<RoleInfoDTO>();
        }
        public List<ActionItems> Actions { get; set; }
        public List<RoleInfoDTO> RolesInfo { get; set; }
    }
    public class RoleInfoDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
