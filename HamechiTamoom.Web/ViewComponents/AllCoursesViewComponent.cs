using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HamechiTamoom.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HamechiTamoom.Web.ViewComponents
{
    public class AllCoursesViewComponent : ViewComponent
    {
        private readonly ICourseService _courseService;

        public AllCoursesViewComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("AllCourses", _courseService.GetCourse()));
        }
    }
}
