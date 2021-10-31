using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HamechiTamoom.Core.DTOs;
using HamechiTamoom.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HamechiTamoom.Web.Pages.Admin.ManageUsers
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;
        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public UsersForAdminViewModel UsersForAdminViewModel { get; set; }

        public void OnGet(int pageId=1, string filterUserName = "", string filterEmail = "")
        {
            UsersForAdminViewModel = _userService.GetUsersByFilter(pageId,filterUserName,filterEmail);
        }

    }
}
