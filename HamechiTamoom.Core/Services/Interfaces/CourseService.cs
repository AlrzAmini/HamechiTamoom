using HamechiTamoom.DataLayer.Entities.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamechiTamoom.DataLayer.Context;

namespace HamechiTamoom.Core.Services.Interfaces
{
    public class CourseService : ICourseService
    {
        private HamechiTamoomContext _context;

        public CourseService(HamechiTamoomContext context)
        {
            _context = context;
        }
        public List<CourseGroup> GetAllCourseGroups()
        {
            return _context.CourseGroups.ToList();
        }
    }
}
