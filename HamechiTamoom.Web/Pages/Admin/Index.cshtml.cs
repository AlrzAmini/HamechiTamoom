using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HamechiTamoom.Core.Consts;
using HamechiTamoom.Core.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HamechiTamoom.Web.Pages.Admin
{
    [PermissionChecker(PerIds.AdminPanel)]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}
