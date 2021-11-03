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
    [PermissionChecker(PerIds.DeleteRole)]
    [Authorize]
    public class DeleteRoleModel : PageModel
    {
        private readonly IPermissionService _permissionService;

        public DeleteRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [BindProperty]
        public Role Role { get; set; }

        public void OnGet(int roleId)
        {
            Role = _permissionService.GetRoleByRoleId(roleId);
        }

        public IActionResult OnPost()
        {
            _permissionService.DeleteRole(Role);

            return RedirectToPage("./Index");
        }
    }
}
