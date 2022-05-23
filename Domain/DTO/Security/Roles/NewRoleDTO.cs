using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Security.Roles
{
    public class NewRoleDTO
    {
        [Required(ErrorMessage = "نام اجباری است")]
        public string Name { get; set; }
    }
}
