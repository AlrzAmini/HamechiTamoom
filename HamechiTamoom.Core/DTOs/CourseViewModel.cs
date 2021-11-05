using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamechiTamoom.DataLayer.Entities.Course;

namespace HamechiTamoom.Core.DTOs
{
    public class ShowCourseForAdminViewModel
    {
        public List<Course> Courses { get; set; }
        public int EpisodeCount { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
    }
}
