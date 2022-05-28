using Domain.DTO.Account.Login;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
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
        public IActionResult Login(string ReturnUrl)
        {
            var loginDTO = new LoginDTO
            {
                ReturnUrl = ReturnUrl
            };

            return View(loginDTO);
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
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, model.Username),
                        new Claim("an", model.Username)
                };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                        new AuthenticationProperties() { IsPersistent = model.RememberMe });

                    if (model.ReturnUrl != null)
                        return LocalRedirect(model.ReturnUrl);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login Failed");
                    return View();
                }


            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("login");
        }

    }
}
