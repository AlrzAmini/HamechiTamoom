using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HamechiTamoom.Core.Convertors;
using HamechiTamoom.Core.DTOs;
using HamechiTamoom.Core.Generator;
using HamechiTamoom.Core.Security;
using HamechiTamoom.Core.Senders;
using HamechiTamoom.Core.Services.Interfaces;
using HamechiTamoom.DataLayer.Entities.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace HamechiTamoom.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IViewRenderService _viewRenderService;

        public AccountController(IUserService userService, IViewRenderService viewRenderService)
        {
            _userService = userService;
            _viewRenderService = viewRenderService;
        }

        #region Register

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel register)
        {
            #region Validations

            if (!ModelState.IsValid)
            {
                return View(register);
            }
            // UserName Exist ?
            if (_userService.IsExistUserName(register.UserName))
            {
                ModelState.AddModelError("UserName", "این نام کاربری قبلا استفاده شده است.");
                return View(register);
            }
            // Email Exist ?
            if (_userService.IsExistEmail(FixText.FixEmail(register.Email)))
            {
                ModelState.AddModelError("Email", "این ایمیل قبلا استفاده شده است.");
                return View(register);
            }

            #endregion

            #region Register User

            // Fill User Fields
            DataLayer.Entities.User.User user = new User()
            {
                ActivationCode = CodeGenerator.GenerateUniqCode(),
                Email = FixText.FixEmail(register.Email),
                IsActive = false,
                Password = PasswordHelper.EncodePasswordMd5(register.Password),
                RegisterDate = DateTime.Now,
                UserAvatar = "Avatar-min.jpg",
                UserName = register.UserName
            };

            // Add new User to Table Users
            _userService.AddUser(user);

            //TODO: Send Activation Email

            #region Send Active Email

            string body = _viewRenderService.RenderToStringAsync("_ActiveEmail", user);
            SendEmail.Send(user.Email,"فعالسازی",body);

            #endregion

            return View("SuccessRegister", user);
            #endregion

        }

        #endregion

        #region Login

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginViewModel login)
        {
            #region Validations

            if (!ModelState.IsValid)
            {
                return View(login);
            }

            #endregion

            #region Login User

            var user = _userService.LoginUser(login);
            if (user != null)
            {
                if (user.IsActive)
                {
                    //TODO: Login user

                    // Chiz haye ke mikhaym az user negahdari knim
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                        new Claim(ClaimTypes.Name,user.UserName)
                    };
                    
                    // Config Identity
                    var identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var properties = new AuthenticationProperties()
                    {
                        IsPersistent = login.RememberMe
                    };

                    // Logging user
                    HttpContext.SignInAsync(principal, properties);

                    ViewBag.IsSuccess = true;
                    return View();
                }
                else
                {
                    ModelState.AddModelError("Email", "حساب کاربری شما فعال نمی باشد.");

                }
            }
            ModelState.AddModelError("Email", "کاربری با این مشخصات یافت نشد.");
            return View(login);

            #endregion
        }

        #endregion

        #region Active Account

        
        public IActionResult ActiveAccount(string id)
        {
            ViewBag.IsActive = _userService.ActiveAccount(id);

            return View();
        }

        #endregion

        #region Logout
        
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        #endregion

        #region Forgot Password

        [Route("ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(ForgotPasswordViewModel forgot)
        {
            if (!ModelState.IsValid)
            {
                return View(forgot);
            }


            string fixedEmail = FixText.FixEmail(forgot.Email);
            DataLayer.Entities.User.User user = _userService.GetUserByEmail(fixedEmail);

            if (user == null)
            {
                ModelState.AddModelError("Email", "ایمیل مورد نظر یافت نشد");
                return View(forgot);
            }


            string emailBody = _viewRenderService.RenderToStringAsync("_ForgotPassword", user);
            SendEmail.Send(user.Email, "بازیابی کلمه عبور",emailBody);
            ViewBag.IsSuccess = true;


            return View();
        }

        #endregion

        #region Reset Password

        public IActionResult ResetPassword(string id) // id = activeCode
        {
            return View(new ResetPasswordViewModel()
            {
                ActiveCode = id
            });
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel reset) 
        {
            if (!ModelState.IsValid)
            {
                return View(reset);
            }

            DataLayer.Entities.User.User user = _userService.GetUserByActiveCode(reset.ActiveCode);

            if (user == null)
            {
                return NotFound();
            }

            string hashNewPassword = PasswordHelper.EncodePasswordMd5(reset.Password);
            user.Password = hashNewPassword;
            _userService.UpdateUser(user);

            return Redirect("/Login");
        }

        #endregion

    }
}
