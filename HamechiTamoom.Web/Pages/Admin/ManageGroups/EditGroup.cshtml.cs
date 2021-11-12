using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HamechiTamoom.Core.Consts;
using HamechiTamoom.Core.Security;
using HamechiTamoom.Core.Services.Interfaces;
using HamechiTamoom.DataLayer.Entities.Course;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HamechiTamoom.Web.Pages.Admin.ManageGroups
{
    [PermissionChecker(PerIds.EditGroup)]
    [Authorize]
    public class EditGroupModel : PageModel
    {
        private readonly ICourseService _courseService;

        public EditGroupModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public CourseGroup CourseGroup { get; set; }


        public void OnGet(int id)
        {
            CourseGroup = _courseService.GetGroupById(id);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            _courseService.UpdateGroup(CourseGroup);

            return RedirectToPage("Index");
        }
    }
}
