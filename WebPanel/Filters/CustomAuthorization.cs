using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace WebPanel.Filters
{
    public class CustomAuthorization : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string _permision;
        private static IUnitOfWork _unitOfWork;
        private static List<UserDomain> _users = new List<UserDomain>();
        private static List<RoleDomain> _roles = new List<RoleDomain>();
        private static List<PermisionDomain> _permisions = new List<PermisionDomain>();


        public CustomAuthorization(string permision)
        {
            _permision = permision;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context == null)
                return;

            var user = context.HttpContext.User;
            if (user == null)
                return;

            if (user.Identity.Name == null)
                return;

            _unitOfWork = (IUnitOfWork)context.HttpContext.RequestServices.GetService(typeof(IUnitOfWork));


            return;
        }
    }
}
