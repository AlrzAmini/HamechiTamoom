using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamechiTamoom.DataLayer.Entities.Permission
{
    public class Permission
    {
        #region Properties

        [Key]
        public int PermissionId { get; set; }

        [DisplayName("عنوان دسترسی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیش از {1} کاراکتر داشته باشد")]
        public string PermissionTitle { get; set; }

        public int? ParrentId { get; set; }

        #endregion

         #region Relations

        // Zir Goroh Haye Hr Permission
        [ForeignKey("ParrentId")]
        public List<Permission>  Permissions { get; set; }

        public List<RolePermission> RolePermissions { get; set; }

        #endregion

    }
}
