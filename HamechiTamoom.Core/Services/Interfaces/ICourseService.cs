using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamechiTamoom.DataLayer.Entities.Course;

namespace HamechiTamoom.Core.Services.Interfaces
{
    public interface ICourseService
    {
        #region Course Group

        List<CourseGroup> GetAllCourseGroups();

        #endregion

    }
}
