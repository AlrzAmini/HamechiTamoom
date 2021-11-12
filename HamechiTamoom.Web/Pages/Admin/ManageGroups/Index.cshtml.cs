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
    [PermissionChecker(PerIds.ManageGroups)]
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ICourseService _courseService;

        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public List<CourseGroup> CourseGroups { get; set; }

        public void OnGet()
        {
            CourseGroups = _courseService.GetGroupsForManage();
        }
    }
}
