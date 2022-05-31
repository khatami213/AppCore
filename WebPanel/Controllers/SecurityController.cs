using Domain.DTO.Security.Roles;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using WebPanel.Filters;
using System.Linq;
using System.Collections.Generic;
using Domain.DTO.Security.Permisions;
using Domain.DTO.Security.RolePermision;

namespace WebPanel.Controllers
{

    public class SecurityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SecurityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Roles

        [HttpGet]
        public async Task<IActionResult> Roles()
        {
            var roleDTO = await _unitOfWork._role.GetAllDTO();

            roleDTO.Actions.Add(new ActionItems() { Title = "ویرایش", Action = "EditRole", Controller = "Security" });
            roleDTO.Actions.Add(new ActionItems() { Title = "مدیریت دسترسی", Action = "Permisions", Controller = "Security" });
            roleDTO.Actions.Add(new ActionItems() { Title = "حذف", Action = "DeleteRole", Controller = "Security" });

            return View(roleDTO);
        }

        [CustomAuthorization()]
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(NewRoleDTO model)
        {
            if (ModelState.IsValid)
            {
                if (_unitOfWork._role.IsDuplicateByName(0, model.Name))
                {
                    ModelState.AddModelError("", "نام نقش تکراری میباشد");
                }
                else
                {
                    await _unitOfWork._role.AddRoleDTO(model);
                    _unitOfWork.Complete();
                    return RedirectToAction("Roles");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(long Id)
        {
            var oldRole = await _unitOfWork._role.GetByID(Id);
            if (oldRole == null)
            {
                ModelState.AddModelError("", "نقشی پیدا نشد");
                return View();
            }
            var updateroleDTO = new UpdateRoleDTO()
            {
                Id = Id,
                Name = oldRole.Name
            };

            return View(updateroleDTO);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(UpdateRoleDTO model)
        {
            if (ModelState.IsValid)
            {
                var oldRole = await _unitOfWork._role.GetByID(model.Id);
                if (oldRole == null)
                {
                    ModelState.AddModelError("", "نقشی پیدا نشد");
                    return View();
                }
                else
                {
                    await _unitOfWork._role.UpdateRoleDTO(model);
                    _unitOfWork.Complete();

                    return RedirectToAction("Roles");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRole(long Id)
        {
            var role = await _unitOfWork._role.GetByID(Id);
            if (role == null)
            {
                ModelState.AddModelError("", "نقشی پیدا نشد");
                return View();
            }

            var deleteRoleDTO = new UpdateRoleDTO()
            {
                Id = Id,
                Name = role.Name
            };

            return View(deleteRoleDTO);
        }
        // [HttpPost]
        //public async Task<IActionResult> DeleteRole(UpdateRoleDTO deleteRole)
        //{
        //    var role = await _unitOfWork._role.GetByID(Id);
        //    if (role == null)
        //    {
        //        ModelState.AddModelError("", "نقشی پیدا نشد");
        //        return View();
        //    }
        //    if (await _unitOfWork._role.DeleteByID(Id))
        //    {
        //        _unitOfWork.Complete();
        //        return RedirectToAction("Roles");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "عملیات با خطا مواجه شد");
        //        return RedirectToAction("Roles");
        //    }
        //}

        #endregion

        #region Permisions

        [HttpGet]
        public async Task<IActionResult> Permisions(long Id)
        {

            var permisions = await _unitOfWork._permision.GetAllPermisionsDTO();

            if (Id == 0)
            {
                ModelState.AddModelError("", "نقشی پیدا نشد");
                return View(permisions);
            }

            var role = await _unitOfWork._role.GetByID(Id);
            if (role == null)
            {
                ModelState.AddModelError("", "نقشی پیدا نشد");
                return View(permisions);
            }

            var rolePermisions = await _unitOfWork._rolePermision.GetAllPermisionsIdByRoleId(role.Id);

            foreach (var item in permisions.Permisions)
            {
                if (rolePermisions.Any(r => r == item.Id))
                    item.IsSelected = true;
            }

            ViewBag.RoleTitle = role.Name;
            permisions.RoleId = role.Id;

            return View(permisions);

        }

        [HttpPost]
        public async Task<IActionResult> Permisions(PermisionDTO model)
        {
            var role = await _unitOfWork._role.GetByID(model.RoleId);

            if (role == null)
            {
                ModelState.AddModelError("", "نقشی پیدا نشد");
                return View(model);
            }

            //Delete Old Permisions

            if (await _unitOfWork._rolePermision.DeletePermisionsByRoleId(role.Id))
            {
                var newRolePermisions = new List<RolePermisionDTO>();
                foreach (var item in model.Permisions)
                {
                    if (item.IsSelected)
                    {
                        newRolePermisions.Add(new RolePermisionDTO()
                        {
                            PermisionId = item.Id,
                            RoleId = role.Id
                        });
                    }
                }
                if (await _unitOfWork._rolePermision.AddRangeRolePermisionInfoDTO(newRolePermisions))
                {
                    _unitOfWork.Complete();
                    return RedirectToAction("Roles");
                }
            }

            return View(model);
        }

        #endregion

        #region Users

        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var users = await _unitOfWork._user.GetAllUsersDTO();

            foreach (var item in users.Users)
            {
                switch (item.UserType)
                {
                    case (int)CoreService.Enums.UserType.Admin:
                        item.UserTypeTitle = "ادمین";
                        break;
                    case (int)CoreService.Enums.UserType.Driver:
                        item.UserTypeTitle = "راننده";
                        break;
                    case (int)CoreService.Enums.UserType.Passenger:
                        item.UserTypeTitle = "مسافر";
                        break;
                }
            }
            users.Actions = new List<ActionItems>() { new ActionItems() { Controller = "security", Action = "UserRoles", Title = "مدیریت نقش ها" } };
            return View(users);
        }
        #endregion

    }
}
