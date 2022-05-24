using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Account.Login
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "نام کاربری الزامی میباشد")]
        public string Username { get; set; }
        [Required(ErrorMessage = "رمز عبور الزامی میباشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
