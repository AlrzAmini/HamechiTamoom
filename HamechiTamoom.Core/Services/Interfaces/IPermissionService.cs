using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamechiTamoom.DataLayer.Entities.User;

namespace HamechiTamoom.Core.Services.Interfaces
{
    public interface IPermissionService
    {
        #region Roles

        List<Role> GetAllRoles();

        void AddRolesToUser(List<int> roleIds, int userId);

        #endregion
    }
}
