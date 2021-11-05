using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HamechiTamoom.Core.Consts;
using HamechiTamoom.Core.Security;
using HamechiTamoom.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HamechiTamoom.DataLayer.Context;
using HamechiTamoom.DataLayer.Entities.Course;

namespace HamechiTamoom.Web.Pages.Admin.ManageCourses
{
    [PermissionChecker(PerIds.DeleteCourse)]
    public class DeleteCourseModel : PageModel
    {
        private ICourseService _courseService;

        public DeleteCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public Course Course { get; set; }

        public IActionResult OnGet(int courseId)
        {

            Course = _courseService.GetCourseById(courseId);

            if (Course == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int courseId)
        {

            if (Course != null)
            {
                if (Course.CourseImageName != null)
                {
                    if (Course.CourseImageName != "DefCourse.jpg")
                    {
                        string imgDeletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Course/image",
                            Course.CourseImageName);
                        if (System.IO.File.Exists(imgDeletePath))
                        {
                            System.IO.File.Delete(imgDeletePath);
                        }

                        string thumbDeletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Course/thumb",
                            Course.CourseImageName);
                        if (System.IO.File.Exists(thumbDeletePath))
                        {
                            System.IO.File.Delete(thumbDeletePath);
                        }
                    }
                }

                _courseService.DeleteCourse(Course);
            }

            return RedirectToPage("./Index");
        }
    }
}
