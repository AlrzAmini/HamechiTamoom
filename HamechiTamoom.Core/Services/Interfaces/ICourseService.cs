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

        List<CourseGroup> GetGroupsForManage();

        List<SelectListItem> GetGroupForManageCourse();

        List<SelectListItem> GetSubGroupForManageCourse(int groupId);

        List<SelectListItem> GetTeachers();

        List<SelectListItem> GetLevels();

        List<SelectListItem> GetStatuses();

        void AddGroup(CourseGroup group);

        void UpdateGroup(CourseGroup group);

        void DeleteGroup(CourseGroup group);

        CourseGroup GetGroupById(int id);


        #endregion

        #region Course

        ShowCourseForAdminViewModel GetCoursesForAdmin(int pageId = 1, string filterCourseTitle = "");

        int AddCourse(Course course,IFormFile imgCourse,IFormFile demoCourse);

        Course GetCourseById(int courseId);

        void UpdateCourse(Course course, IFormFile imgCourse, IFormFile demoCourse);

        void DeleteCourse(Course course);

        List<ShowCourseListViewModel> GetCourse(int pageId = 1, string filter = "", string getType = "all",
            string orderByType = "date", int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 0);

        Course GetCourseForShow(int courseId);

        #endregion

        #region Episode

        List<CourseEpisode> GetListEpisodesCourse(int courseId);

        int AddEpisode(CourseEpisode episode, IFormFile episodeFile);

        bool CheckExistFile(string fileName);

        CourseEpisode GetEpisodeByEpisodeId(int episodeId);

        void UpdateEpisode(CourseEpisode episode, IFormFile episodeFile);

        void DeleteEpisode(CourseEpisode episode);


        #endregion

        #region Comments

        void AddComment(AllCmnt comment);

        Tuple<List<AllCmnt>,int> GetCourseComment(int courseId , int pageId=1);

        #endregion

    }
}
