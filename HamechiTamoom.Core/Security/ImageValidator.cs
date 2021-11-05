using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HamechiTamoom.Core.Security
{
    public static class ImageValidator
    {
        public static bool IsImage(this IFormFile value)
        {
            try
            {
                var img = System.Drawing.Image.FromStream(value.OpenReadStream());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
