using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HamechiTamoom.Core.DTOs;
using HamechiTamoom.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace HamechiTamoom.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        #region User Panel Index

        public IActionResult Index()
        {
            return View(_userService.GetUserInformation(User.Identity.Name));
        }

        #endregion

        #region Edit User Information

        [Route("UserPanel/EditProfile")]
        public IActionResult EditProfile()
        {
            return View(_userService.GetUserDataForEditProfile(User.Identity.Name));
        }

        [HttpPost]
        [Route("UserPanel/EditProfile")]
        public IActionResult EditProfile(EditUserProfileViewModel profile)
        {
            if (!ModelState.IsValid)
            {
                return View(profile);
            }

            _userService.EditProfile(User.Identity.Name,profile);

            // Log Out User
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/Login?EditProfile=True");
        }

        #endregion

        #region Change Password

        [Route("UserPanel/ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }


        [Route("UserPanel/ChangePassword")]
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel change)
        {
            string currentUserName = User.Identity.Name;

            if (!ModelState.IsValid)
            {
                return View(change);
            }

            if (!_userService.CompareOldPassword(change.OldPassword, currentUserName))
            {
                ModelState.AddModelError("OldPassword", "کلمه عبور فعلی صحیح نمیباشد");
                return View(change);
            }

            _userService.ChangePassword(currentUserName,change.Password);
            ViewBag.IsSuccess = true;

            return View();
        }

        #endregion

    }
}
