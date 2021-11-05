using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HamechiTamoom.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HamechiTamoom.Web.Pages.Admin.Controllers
{
    public class HomeAdmin : Controller
    {
        private ICourseService _courseService;

        public HomeAdmin(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public IActionResult GetSubGroups(int id)
        {
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب کنید",Value = ""}
            };
            list.AddRange(_courseService.GetSubGroupForManageCourse(id));
            return Json(new SelectList(list, "Value", "Text"));
        }
    }
}
