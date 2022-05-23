using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Security.Roles
{
    public class UpdateRoleDTO
    {
        [Required(ErrorMessage = "شناسه الزامی است")]
        public long Id { get; set; }
        [Required(ErrorMessage = "نام الزامی است")]
        public string Name { get; set; }
    }
}
