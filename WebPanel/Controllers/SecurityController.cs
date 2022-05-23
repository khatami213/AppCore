using Domain.DTO.Security.Roles;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Domain.Model;

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
            roleDTO.Actions.Add(new ActionItems() { Title = "حذف", Action = "DeleteRole", Controller = "Security" });

            return View(roleDTO);
        }

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

            if (await _unitOfWork._role.DeleteByID(Id))
            {
                _unitOfWork.Complete();
                return RedirectToAction("Roles");
            }
            else
            {
                ModelState.AddModelError("", "عملیات با خطا مواجه شد");
                return RedirectToAction("Roles");
            }

        }

        #endregion
    }
}
