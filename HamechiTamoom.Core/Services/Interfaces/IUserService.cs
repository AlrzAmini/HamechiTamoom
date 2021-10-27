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
        bool IsExistUserName(string userName);

        bool IsExistEmail(string userName);

        int AddUser(User user);

        User LoginUser(LoginViewModel login);

        bool ActiveAccount(string activeCode); 

    }
}
