using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamechiTamoom.DataLayer.Entities.User
{
    public class UserRole
    {
        public UserRole()
        {
            
        }

        #region Properties

        [Key]
        public int UserRoleId { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }

        #endregion

        #region Relations
        //[ForeignKey("UserId")]
        public User User { get; set; }

        //[ForeignKey("RoleId")]
        public Role Role { get; set; }

        #endregion

    }
}
