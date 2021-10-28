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

        #endregion

        #region UserPanel

        InformationUserViewModel GetUserInformation(string userName);

        #endregion

    }
}
