using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HamechiTamoom.Core.Consts;
using HamechiTamoom.Core.DTOs;
using HamechiTamoom.Core.Security;
using HamechiTamoom.Core.Services.Interfaces;
using HamechiTamoom.DataLayer.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HamechiTamoom.Web.Pages.Admin.ManageUsers
{
    [PermissionChecker(PerIds.EditUser)]
    public class EditUserModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;

        public EditUserModel(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }

        [BindProperty]
        public EditUserViewModel EditUserViewModel { get; set; }
        [BindProperty]
        public List<Role> Roles { get; set; }

        public void OnGet(int userId)
        {
            EditUserViewModel = _userService.GetUserForShowInEditMode(userId);
            //ViewData["Roles"] = _permissionService.GetAllRoles();
            Roles = _permissionService.GetAllRoles();
        }

        public IActionResult OnPost(List<int> SelectedRoles)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Edit User Information
            _userService.EditUserFromAdmin(EditUserViewModel);

            // Edit User Roles
            _permissionService.EditRolesUser(SelectedRoles,EditUserViewModel.UserId);

            return RedirectToPage("Index");
        }
    }
}
