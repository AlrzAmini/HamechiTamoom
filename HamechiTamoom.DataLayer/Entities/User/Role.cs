using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamechiTamoom.DataLayer.Entities.User
{
    public class Role
    {
        public Role()
        {
            
        }

        #region RoleID

        [Key]
        public int RoleId { get; set; }

        #endregion

        #region RoleTitle

        [DisplayName("عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(150,ErrorMessage = "{0} نمیتواند بیش از {1} کاراکتر داشته باشد")]
        public string RoleTitle { get; set; }

        #endregion

        #region Relations

        public List<UserRole> UserRoles { get; set; }

        #endregion
    }
}
