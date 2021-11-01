using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HamechiTamoom.Core.DTOs;
using HamechiTamoom.Core.Services.Interfaces;
using HamechiTamoom.DataLayer.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HamechiTamoom.Web.Pages.Admin.ManageUsers
{
    public class CreateUserModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;

        public CreateUserModel(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }

        [BindProperty]
        public CreateUserViewModel CreateUserViewModel { get; set; }

        [BindProperty]
        public List<Role> Roles { get; set; }
        

        public void OnGet()
        {
            Roles = _permissionService.GetAllRoles();
            //ViewData["Roles"] = _permissionService.GetAllRoles();
        }

        public IActionResult OnPost(List<int> SelectedRoles)
        {
            #region Validation

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (SelectedRoles == null)
            {
                ModelState.AddModelError("", "لطفا تمامی فبلد های قرار گرفته را پر کنید");
            }

            if (_userService.IsExistUserName(CreateUserViewModel.UserName))
            {
                ModelState.AddModelError("", "این نام کاربری قبلا استفاده شده است.");
                ViewData["IsUserNameUniq"] = "false";
                return Page();
            }

            if (_userService.IsExistEmail(CreateUserViewModel.Email))
            {
                ModelState.AddModelError("", "این ایمیل قبلا استفاده شده است.");
                ViewData["IsEmailUniq"] = "false";
                return Page();
            }

            #endregion


            int userId = _userService.AddUserFromAdmin(CreateUserViewModel);

            // Add Roles to user
            _permissionService.AddRolesToUser(SelectedRoles,userId);

            return Redirect("/Admin/ManageUsers");
        }
    }
}
