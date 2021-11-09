using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HamechiTamoom.Core.Services.Interfaces;
using HamechiTamoom.DataLayer.Entities.Course;

namespace HamechiTamoom.Web.Controllers
{
    public class CourseController : Controller
    {
        private ICourseService _courseService;
        private IUserService _userService;

        public CourseController(ICourseService courseService, IUserService userService)
        {
            _courseService = courseService;
            _userService = userService;
        }

        public IActionResult Index(int pageId = 1, string filter = ""
            , string getType = "all", string orderByType = "date",
            int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null)
        {
            ViewBag.selectedGroups = selectedGroups;
            ViewBag.Groups = _courseService.GetAllCourseGroups();
            ViewBag.pageId = pageId;
            return View(_courseService.GetCourse(pageId, filter, getType, orderByType, startPrice, endPrice, selectedGroups, 9));
        }


        [Route("ShowCourse/{id}")]
        public IActionResult ShowCourse(int id)
        {
            Course course = _courseService.GetCourseForShow(id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [Route("DownloadFile/{episodeId}")]
        public IActionResult DownloadFile(int episodeId)
        {
            var episode = _courseService.GetEpisodeByEpisodeId(episodeId);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/EpisodeFiles",
                episode.EpisodeFileName);
            string fileName = episode.EpisodeFileName;


            if (User.Identity.IsAuthenticated)
            {
                byte[] file = System.IO.File.ReadAllBytes(filePath);
                return File(file, "application/force-download", fileName);
            }


            return Forbid();
        }


        [HttpPost]
        public IActionResult CreateComment(AllCmnt courseComment)
        {
            courseComment.CreateDate = DateTime.Now;
            courseComment.UserId = _userService.GetUserIdByUserName(User.Identity.Name);

            _courseService.AddComment(courseComment);

            return View("ShowComment", _courseService.GetCourseComment(courseComment.CourseId));
        }

        public IActionResult ShowComment(int id,int pageId = 1)
        {
            return View(_courseService.GetCourseComment(id, pageId));
        }
    }
}
