using Domain.DTO.Account.Login;
using Domain.DTO.Account.Register;
using Domain.DTO.Security.User;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserDomain : IGenericDomain<UserDomain>
    {
        Task<LoginDTO> GetByUsername(string username);
        Task<bool> CheckPassword(string username, string password);
        Task<bool> IsDuplicateByUsername(string username, long id);
        Task<bool> IsDuplicateByUsernameAndUserType(string username, int userType, long id);
        Task<bool> RegisterUserDTO(RegisterDTO registerDTO);
        Task<UserDTO> GetAllUsersDTO();
        Task<LoginDTO> GetUsernameAndType(string username, int userType);
    }
}
