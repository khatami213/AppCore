using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Security.User
{
    public class UserDTO
    {
        public UserDTO()
        {
            Actions = new List<ActionItems>();
            Users = new List<UserInfoDTO>();
        }
        public List<UserInfoDTO> Users { get; set; }
        public List<ActionItems> Actions { get; set; }
    }
    public class UserInfoDTO
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public int UserType { get; set; }
        public string UserTypeTitle { get; set; }
    }
}
