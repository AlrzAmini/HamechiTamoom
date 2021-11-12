using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HamechiTamoom.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HamechiTamoom.Web.ViewComponents
{
    public class LatesCourseViewComponent : ViewComponent
    {
        private ICourseService _courseService;

        public LatesCourseViewComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("LatesCourse", _courseService.GetCourse()));
        }
    }
}
