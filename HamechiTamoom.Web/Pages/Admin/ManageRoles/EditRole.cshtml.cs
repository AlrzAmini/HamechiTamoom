using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HamechiTamoom.Core.Consts;
using HamechiTamoom.Core.Security;
using HamechiTamoom.Core.Services.Interfaces;
using HamechiTamoom.DataLayer.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HamechiTamoom.Web.Pages.Admin.ManageRoles
{
    [PermissionChecker(PerIds.EditRole)]
    [Authorize]
    public class EditRoleModel : PageModel
    {
        private readonly IPermissionService _permissionService;

        public EditRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [BindProperty]
        public Role Role { get; set; }

        public void OnGet(int roleId)
        {
            Role = _permissionService.GetRoleByRoleId(roleId);
            ViewData["Permissions"] = _permissionService.GetAllPermissions();
            ViewData["SelectedPermissions"] = _permissionService.PermissionsRole(roleId);
        }

        public IActionResult OnPost(List<int> SelectedPermission)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _permissionService.UpdateRole(Role);

            _permissionService.EditPermissionsRole(Role.RoleId,SelectedPermission);

            return RedirectToPage("./Index");
        }
    }
}
