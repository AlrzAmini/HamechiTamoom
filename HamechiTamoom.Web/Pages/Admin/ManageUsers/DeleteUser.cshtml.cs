using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HamechiTamoom.Core.Consts;
using HamechiTamoom.Core.DTOs;
using HamechiTamoom.Core.Security;
using HamechiTamoom.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HamechiTamoom.DataLayer.Context;
using HamechiTamoom.DataLayer.Entities.User;

namespace HamechiTamoom.Web.Pages.Admin.ManageUsers
{
    [PermissionChecker(PerIds.DeleteUser)]
    public class DeleteModel : PageModel
    {
        private readonly IUserService _userService;

        public DeleteModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public InformationUserViewModel Information { get; set; }

        public IActionResult OnGet(int userId)
        {
            Information = _userService.GetUserInformation(userId);

            ViewData["UserId"] = userId;

            if (Information == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost(int userId)
        {
            Information = _userService.GetUserInformation(userId);

            if (Information != null)
            {
                if (Information.UserAvatar != null)
                {
                    if (Information.UserAvatar != "Avatar-min.jpg")
                    {
                        string deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/UserAvatar",
                            Information.UserAvatar);
                        if (System.IO.File.Exists(deletePath))
                        {
                            System.IO.File.Delete(deletePath);
                        }
                    }
                }
                _userService.DeleteUserFromAdmin(userId);
            }

            return RedirectToPage("./Index");
        }
    }
}
