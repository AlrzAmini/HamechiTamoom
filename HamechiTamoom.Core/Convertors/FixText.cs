using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamechiTamoom.Core.Convertors
{
    public class FixText
    {
        public static string FixEmail( string email)
        {
            return email.Trim().ToLower();
        }
    }
}
