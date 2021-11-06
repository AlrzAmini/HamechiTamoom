using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HamechiTamoom.Core.Services.Interfaces;
using HamechiTamoom.DataLayer.Entities.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HamechiTamoom.Web.Pages.Admin.ManageCourses.Episodes
{
    public class IndexEpisodeModel : PageModel
    {
        private readonly ICourseService _courseService;

        public IndexEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public List<CourseEpisode> CourseEpisodesList { get; set; }
        public void OnGet(int courseId)
        {
            ViewData["CourseId"] = courseId;

            CourseEpisodesList = _courseService.GetListEpisodesCourse(courseId);
        }
    }
}
