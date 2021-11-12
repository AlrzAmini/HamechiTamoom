using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HamechiTamoom.Core.Services.Interfaces;
using HamechiTamoom.DataLayer.Entities.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HamechiTamoom.Web.Pages.Admin.ManageGroups
{
    public class DeleteGroupModel : PageModel
    {
        private readonly ICourseService _courseService;

        public DeleteGroupModel(ICourseService courseService)
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
            _courseService.DeleteGroup(CourseGroup);

            return RedirectToPage("Index");
        }
    }
}
