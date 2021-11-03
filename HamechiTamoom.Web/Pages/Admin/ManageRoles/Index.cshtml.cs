using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HamechiTamoom.Core.Consts;
using HamechiTamoom.Core.DTOs;
using HamechiTamoom.Core.Security;
using HamechiTamoom.Core.Services.Interfaces;
using HamechiTamoom.DataLayer.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HamechiTamoom.Web.Pages.Admin.ManageRoles
{
    [PermissionChecker(PerIds.ManageRoles)]
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IPermissionService _permissionService;

        public IndexModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [BindProperty]
        public List<Role> RolesList { get; set; }

        public void OnGet()
        {
            RolesList = _permissionService.GetAllRoles();
        }
    }
}
