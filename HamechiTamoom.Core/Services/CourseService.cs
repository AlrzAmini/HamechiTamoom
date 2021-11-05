using HamechiTamoom.DataLayer.Entities.Course;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamechiTamoom.Core.Convertors;
using HamechiTamoom.Core.Generator;
using HamechiTamoom.Core.Services.Interfaces;
using HamechiTamoom.DataLayer.Context;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using HamechiTamoom.Core.DTOs;
using HamechiTamoom.Core.Security;

namespace HamechiTamoom.Core.Services
{
    public class CourseService : ICourseService
    {
        private readonly HamechiTamoomContext _context;

        public CourseService(HamechiTamoomContext context)
        {
            _context = context;
        }



        public List<CourseGroup> GetAllCourseGroups()
        {
            return _context.CourseGroups.ToList();
        }

        public List<SelectListItem> GetGroupForManageCourse()
        {
            return _context.CourseGroups.Where(g => g.ParrentId == null)
                .Select(g => new SelectListItem()
                {
                    Text = g.GroupTitle,
                    Value = g.GroupId.ToString()
                }).ToList();
        }

        public List<SelectListItem> GetSubGroupForManageCourse(int groupId)
        {
            return _context.CourseGroups.Where(g => g.ParrentId == groupId)
                .Select(g => new SelectListItem()
                {
                    Text = g.GroupTitle,
                    Value = g.GroupId.ToString()
                }).ToList();
        }

        public List<SelectListItem> GetTeachers()
        {
            return _context.UserRoles.Where(r => r.RoleId == 2)
                .Include(r => r.User)
                .Select(u => new SelectListItem()
                {
                    Value = u.UserId.ToString(),
                    Text = u.User.UserName
                }).ToList();
        }

        public List<SelectListItem> GetLevels()
        {
            return _context.CourseLevels.Select(l => new SelectListItem()
            {
                Value = l.LevelId.ToString(),
                Text = l.LevelTitle
            }).ToList();
        }

        public List<SelectListItem> GetStatuses()
        {
            return _context.CourseStatus.Select(s => new SelectListItem()
            {
                Value = s.StatusId.ToString(),
                Text = s.StatusTitle
            }).ToList();
        }

        public int AddCourse(Course course, IFormFile imgCourse, IFormFile demoCourse)
        {
            course.CreateDate = DateTime.Now;
            course.CourseImageName = "DefCourse.jpg"; // to halat defualt in ax hst Magar ...
            //TODO: check image
            if (imgCourse != null && imgCourse.IsImage())
            {

                course.CourseImageName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(imgCourse.FileName);

                string imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Course/image",
                    course.CourseImageName);
                using (var stream = new FileStream(imgPath, FileMode.Create))
                {
                    imgCourse.CopyTo(stream);
                }

                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Course/thumb",
                    course.CourseImageName);
                ImageConvertor imgResizer = new ImageConvertor();
                imgResizer.Image_resize(imgPath,thumbPath,70);
            }

            if (demoCourse != null)
            {
                course.DemoFileName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(demoCourse.FileName);
                string demoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/Demos",
                    course.DemoFileName);
                using (var stream = new FileStream(demoPath, FileMode.Create))
                {
                    demoCourse.CopyTo(stream);
                }
            }

            _context.Courses.Add(course);
            _context.SaveChanges();

            return course.CourseId;
        }

        public ShowCourseForAdminViewModel GetCoursesForAdmin(int pageId = 1, string filterCourseTitle = "")
        {
            IQueryable<Course> result = _context.Courses;

            // filter list by course title
            if (!string.IsNullOrEmpty(filterCourseTitle))
            {
                result = result.Where(c => c.CourseTitle.Contains(filterCourseTitle));
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            var ep = result.Include(e => e.CourseEpisodes).First();
            int epCount = ep.CourseEpisodes.Count();

            ShowCourseForAdminViewModel list = new ShowCourseForAdminViewModel();
            list.CurrentPage = pageId;
            list.TotalPage = (int)Math.Ceiling((decimal)result.Count() / take);
            list.Courses = result.OrderBy(c => c.CreateDate).Skip(skip).Take(take).ToList();
            list.EpisodeCount = epCount;

            return list;
        }

    }
}
