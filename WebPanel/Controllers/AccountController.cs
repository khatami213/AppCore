using Domain.DTO.Account.Login;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebPanel.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                var userDTO = await _unitOfWork._user.GetByUsername(model.Username);
                if (userDTO == null)
                {
                    ModelState.AddModelError("", "نام کاربری پیدا نشد");
                    return View(model);
                }

                if (await _unitOfWork._user.CheckPassword(model.Username, model.Password))
                {
                    ModelState.AddModelError("", "Login Success");
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Login Failed");
                    return View();
                }


            }
            return View(model);
        }

    }
}
