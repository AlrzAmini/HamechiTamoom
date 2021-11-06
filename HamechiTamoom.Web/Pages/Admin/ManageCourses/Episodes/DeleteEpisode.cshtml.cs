using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HamechiTamoom.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HamechiTamoom.DataLayer.Context;
using HamechiTamoom.DataLayer.Entities.Course;
using Microsoft.AspNetCore.Http;

namespace HamechiTamoom.Web.Pages.Admin.ManageCourses.Episodes
{
    public class DeleteEpisodeModel : PageModel
    {
        private ICourseService _courseService;

        public DeleteEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public CourseEpisode CourseEpisode { get; set; }

        public IActionResult OnGet(int id)
        {

            CourseEpisode = _courseService.GetEpisodeByEpisodeId(id);

            if (CourseEpisode == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            if (CourseEpisode != null)
            {
                // delete episode file
                string deleteFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/EpisodeFiles",
                    CourseEpisode.EpisodeFileName);
                System.IO.File.Delete(deleteFilePath);

                // delete episode
                _courseService.DeleteEpisode(CourseEpisode);
            }

            return Redirect("/Admin/ManageCourses");
        }
    }
}
