using Domain.DTO.Account.Login;
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
    }
}
