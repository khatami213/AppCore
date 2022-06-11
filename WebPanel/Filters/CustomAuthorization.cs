using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPanel.Filters
{
    public class CustomAuthorization : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly string _permision;
        private readonly string _roles;
        private static IUnitOfWork _unitOfWork;
        private static List<UserDomain> _databaseUsers = new List<UserDomain>();
        private static List<RoleDomain> _databaseRoles = new List<RoleDomain>();
        private static List<PermisionDomain> _databasePermisions = new List<PermisionDomain>();
        private static List<UserRoleDomain> _databaseUserRoles = new List<UserRoleDomain>();
        private static List<RolePermisionDomain> _databaseRolePermisions = new List<RolePermisionDomain>();


        public CustomAuthorization(string permision, string roles)
        {
            _permision = permision;
            _roles = roles;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context == null)
                return;


            if (context.HttpContext.User == null)
                return;

            if (context.HttpContext.User.Identity.Name == null)
                return;

            _unitOfWork = (IUnitOfWork)context.HttpContext.RequestServices.GetService(typeof(IUnitOfWork));

            #region Initials 

            if (_databaseUsers.Count == 0)
            {
                var u = await _unitOfWork._user.GetAll();
                _databaseUsers = u.ToList();
            }

            if (_databaseRoles.Count == 0)
            {
                var r = await _unitOfWork._role.GetAll();
                _databaseRoles = r.ToList();
            }

            if (_databasePermisions.Count == 0)
            {
                var p = await _unitOfWork._permision.GetAll();
                _databasePermisions = p.ToList();
            }

            if (_databaseUserRoles.Count == 0)
            {
                var ur = await _unitOfWork._userRole.GetAll();
                _databaseUserRoles = ur.ToList();
            }

            if (_databaseRolePermisions.Count == 0)
            {
                var rp = await _unitOfWork._rolePermision.GetAll();
                _databaseRolePermisions = rp.ToList();
            }

            #endregion

            var user = _databaseUsers.FirstOrDefault(r => r.Username == context.HttpContext.User.Identity.Name);
            if (user == null)
                return;

            if (user.IsAdmin)
                return;

            //get userRoles
            var userRoleIds = _databaseUserRoles.Where(r => r.UserId == user.Id).Select(r => r.RoleId).ToList();

            if (userRoleIds == null)
                return;


            //get roles of user
            var roles = _databaseRoles.Where(r => userRoleIds.Contains(r.Id)).ToList();
            if (roles == null)
                return;

            //get rolePermisions
            var rolePermisions = _databaseRolePermisions.Where(r => roles.Select(x => x.Id).Contains(r.RoleId)).Select(r => r.PermisionId).ToList();
            if (rolePermisions == null)
                return;

            //get Permisions
            var permisions = _databasePermisions.Where(r => rolePermisions.Contains(r.Id)).ToList();
            if (permisions == null)
                return;

            if (!permisions.Any(r => r.Value == _permision))
                context.Result = new ForbidResult();


            if (_roles.Trim(',') != string.Empty)
            {
                var arrRoles = _roles.Split(",");

                foreach (var item in arrRoles)
                {
                    if (!roles.Any(r => r.Name.ToLower() == item.ToLower()))
                    {
                        context.Result = new ForbidResult();
                        break;
                    }
                }
            }

            return;
        }
    }
}
