using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HamechiTamoom.Core.Consts;
using HamechiTamoom.Core.Security;
using HamechiTamoom.Core.Services.Interfaces;
using HamechiTamoom.DataLayer.Entities.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HamechiTamoom.Web.Pages.Admin.ManageCourses
{
    [PermissionChecker(PerIds.EditCourse)]
    public class EditCourseModel : PageModel
    {
        private readonly ICourseService _courseService;

        public EditCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public Course Course { get; set; }

        public void OnGet(int courseId)
        {
            Course = _courseService.GetCourseById(courseId);

            var groups = _courseService.GetGroupForManageCourse();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text", Course.GroupId);

            var subGroups = _courseService.GetSubGroupForManageCourse(Course.GroupId);
            ViewData["SubGroups"] = new SelectList(subGroups, "Value", "Text", Course.SubGroup??0);

            var teachers = _courseService.GetTeachers();
            ViewData["Teachers"] = new SelectList(teachers, "Value", "Text", Course.TeacherId);

            var levels = _courseService.GetLevels();
            ViewData["Levels"] = new SelectList(levels, "Value", "Text", Course.LevelId);

            var statuses = _courseService.GetStatuses();
            ViewData["Statuses"] = new SelectList(statuses, "Value", "Text", Course.StatusId);

        }

        public IActionResult OnPost(IFormFile imgCourseUp, IFormFile demofileUp)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _courseService.UpdateCourse(Course,imgCourseUp,demofileUp);

            return RedirectToPage("Index");
        }
    }
}
