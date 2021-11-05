using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HamechiTamoom.Core.Consts;
using HamechiTamoom.Core.DTOs;
using HamechiTamoom.Core.Security;
using HamechiTamoom.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HamechiTamoom.Web.Pages.Admin.ManageCourses
{
    [PermissionChecker(PerIds.ManageCourses)]
    public class IndexModel : PageModel
    {
        private readonly ICourseService _courseService;

        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public ShowCourseForAdminViewModel ShowCourseForAdminViewModel { get; set; }

        public void OnGet(int pageId = 1, string filterCourseTitle = "")
        {
            ShowCourseForAdminViewModel = _courseService.GetCoursesForAdmin(pageId, filterCourseTitle);
        }
    }
}
