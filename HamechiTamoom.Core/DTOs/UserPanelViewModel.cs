using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamechiTamoom.Core.DTOs
{
    public class InformationUserViewModel
    {
        #region User Information

        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime RegisterDate { get; set; }
        public int Wallet { get; set; }

        #endregion
    }
}
