using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamechiTamoom.DataLayer.Entities.User;
using Microsoft.AspNetCore.Http;

namespace HamechiTamoom.Core.DTOs
{
    public class UsersForAdminViewModel
    {
        #region For Paging

        public List<User> Users { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }

        #endregion

    }

    public class CreateUserViewModel
    {
        #region UserName

        [DisplayName("نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیش از {1} کاراکتر داشته باشد")]
        public string UserName { get; set; }

        #endregion

        #region Email

        [DisplayName("ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(400, ErrorMessage = "{0} نمیتواند بیش از {1} کاراکتر داشته باشد")]
        [EmailAddress(ErrorMessage = "ایمیل دارد شده معتبر نیست . دقت کنید !")]
        public string Email { get; set; }

        #endregion 

        #region Password 

        [DisplayName("کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیش از {1} کاراکتر داشته باشد")]
        public string Password { get; set; }

        #endregion

        #region User Avatar

        public IFormFile UserAvatar { get; set; }

        #endregion

        #region User Roles

        //public List<int> SelectedRoles { get; set; }

        #endregion
    }

    public class EditUserViewModel
    {
        #region User Id

        public int UserId { get; set; }

        #endregion
        #region UserName

        public string UserName { get; set; }

        #endregion

        #region Email

        [DisplayName("ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(400, ErrorMessage = "{0} نمیتواند بیش از {1} کاراکتر داشته باشد")]
        [EmailAddress(ErrorMessage = "ایمیل دارد شده معتبر نیست . دقت کنید !")]
        public string Email { get; set; }

        #endregion 

        #region Password 

        [DisplayName("کلمه عبور")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیش از {1} کاراکتر داشته باشد")]
        public string Password { get; set; }

        #endregion

        #region User Avatar

        public IFormFile UserAvatar { get; set; }

        public string AvatarName { get; set; }

        #endregion

        #region User Roles

        public List<int> UserRoles { get; set; }

        #endregion
    }

}
