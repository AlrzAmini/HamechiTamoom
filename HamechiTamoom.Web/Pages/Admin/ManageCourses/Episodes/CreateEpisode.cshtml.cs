using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HamechiTamoom.Core.Services.Interfaces;
using HamechiTamoom.DataLayer.Entities.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HamechiTamoom.Web.Pages.Admin.ManageCourses.Episodes
{
    public class CreateEpisodeModel : PageModel
    {
        private readonly ICourseService _courseService;

        public CreateEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public CourseEpisode CourseEpisode { get; set; }

        public void OnGet(int courseId)
        {
            CourseEpisode = new CourseEpisode();
            CourseEpisode.CourseId = courseId;
        }

        public IActionResult OnPost(IFormFile episodeFileUp)
        {
            if (!ModelState.IsValid || episodeFileUp == null)
            {
                return Page();
            }

            if (_courseService.CheckExistFile(episodeFileUp.FileName))
            {
                ViewData["IsFileNameExist"] = true;
                return Page();
            }

            _courseService.AddEpisode(CourseEpisode, episodeFileUp);

            return Redirect("/Admin/ManageCourses/Episodes/IndexEpisode?courseId=" + CourseEpisode.CourseId);
        }
    }
}
