using Domain.DTO.Account.Login;
using Domain.DTO.Account.Register;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using static CoreService.Enums;

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
                var userDTO = await _unitOfWork._user.GetUsernameAndType(model.Username, model.UserType);
                if (userDTO == null)
                {
                    ModelState.AddModelError("", "نام کاربری پیدا نشد");
                    return View(model);
                }

                if (await _unitOfWork._user.CheckPassword(model.Username, model.Password))
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, userDTO.Username),
                        new Claim("UserType", userDTO.UserType.ToString()),
                        new Claim("UserId", userDTO.UserId.ToString()),

                };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                        new AuthenticationProperties() { IsPersistent = model.RememberMe });

                    if (model.ReturnUrl != null)
                        return LocalRedirect(model.ReturnUrl);
                    else
                    {
                        switch (userDTO.UserType)
                        {
                            case (int)UserType.Admin:
                                return RedirectToAction("index", "admin");
                            case (int)UserType.Driver:
                                return RedirectToAction("index", "driver");
                            case (int)UserType.Passenger:
                                return RedirectToAction("index", "passenger");
                            default:
                                return RedirectToAction("AccessDenied");
                        }
                    }

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

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                if (await _unitOfWork._user.IsDuplicateByUsernameAndUserType(model.Username, model.UserType, 0))
                {
                    ModelState.AddModelError("", "نام کاربری تکراری میباشد");
                    return View(model);
                }

                if (await _unitOfWork._user.RegisterUserDTO(model))
                {
                    _unitOfWork.Complete();

                    var claims = new List<Claim>() { new Claim(ClaimTypes.Name, model.Username) };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                        new AuthenticationProperties() { IsPersistent = model.RememberMe });

                    return RedirectToAction("Index", "home");
                }
                else
                {
                    ModelState.AddModelError("", "عملیات با خطا مواجه شد");
                    return View(model);
                }

            }
            return View(model);
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
