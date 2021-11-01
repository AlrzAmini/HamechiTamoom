using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamechiTamoom.Core.DTOs;
using HamechiTamoom.DataLayer.Entities.User;

namespace HamechiTamoom.Core.Services.Interfaces
{
    public interface IUserService
    {
        #region Register

        bool IsExistUserName(string userName);

        bool IsExistEmail(string userName);

        int AddUser(User user);

        #endregion

        #region Login

        User LoginUser(LoginViewModel login);

        bool ActiveAccount(string activeCode);

        void UpdateUser(User user);

        #endregion

        #region GetUserBy ...

        User GetUserByEmail(string email);

        User GetUserByUserName(string userName);

        User GetUserByActiveCode(string activeCode);

        User GetUserByUserId(int userId);

        #endregion

        #region User Panel

        InformationUserViewModel GetUserInformation(string userName);

        SideBarUserPanelViewModel GetSideBarUserPanelData(string userName);

        EditUserProfileViewModel GetUserDataForEditProfile(string userName);

        void EditProfile(string username, EditUserProfileViewModel profile);

        bool CompareOldPassword(string oldPassword, string username);

        void ChangePassword(string userName, string newPassword);

        #endregion

        #region Admin Panel

        UsersForAdminViewModel GetUsersByFilter(int pageId = 1, string filterUserName = "", string filterEmail = "");

        int AddUserFromAdmin(CreateUserViewModel user);

        EditUserViewModel GetUserForShowInEditMode(int userId);

        void EditUserFromAdmin(EditUserViewModel editUser);

        #endregion

    }
}
