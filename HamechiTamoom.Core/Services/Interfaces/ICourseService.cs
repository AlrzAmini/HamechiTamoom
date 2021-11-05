using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamechiTamoom.Core.DTOs;
using HamechiTamoom.DataLayer.Entities.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HamechiTamoom.Core.Services.Interfaces
{
    public interface ICourseService
    {
        #region Course Group

        List<CourseGroup> GetAllCourseGroups();

        List<SelectListItem> GetGroupForManageCourse();

        List<SelectListItem> GetSubGroupForManageCourse(int groupId);

        List<SelectListItem> GetTeachers();

        List<SelectListItem> GetLevels();

        List<SelectListItem> GetStatuses();

        #endregion

        #region Course

        ShowCourseForAdminViewModel GetCoursesForAdmin(int pageId = 1, string filterCourseTitle = "");

        int AddCourse(Course course,IFormFile imgCourse,IFormFile demoCourse);

        #endregion

    }
}
