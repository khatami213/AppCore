using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
            var roles = await _unitOfWork._role.GetAllDTO();

            return View(roles);
        }

        #endregion
    }
}
