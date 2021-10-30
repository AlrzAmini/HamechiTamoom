using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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

    public class SideBarUserPanelViewModel
    {
        public string UserName { get; set; }
        public string ImageName { get; set; } = "-";
    }

    public class EditUserProfileViewModel
    {
        #region UserName

        [DisplayName("نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیش از {1} کاراکتر داشته باشد")]
        public string UserName { get; set; }

        #endregion

        #region Avatar

        public IFormFile UserAvatar { get; set; }

        public string CurrentAvatar { get; set; }

        #endregion

    }

    public class ChangePasswordViewModel
    {
        #region Password & Repeation Password & OldPassword

        [DisplayName("کلمه عبور فعلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیش از {1} کاراکتر داشته باشد")]
        public string OldPassword { get; set; }

        [DisplayName("کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیش از {1} کاراکتر داشته باشد")]
        public string Password { get; set; }

        [DisplayName("تکرار کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیش از {1} کاراکتر داشته باشد")]
        [Compare("Password", ErrorMessage = "کلمه عبور و تکرار آن مغایرت دارند.")]
        public string RePassword { get; set; }

        #endregion
    }
}
