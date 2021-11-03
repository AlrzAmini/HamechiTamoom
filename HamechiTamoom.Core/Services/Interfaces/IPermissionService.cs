using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamechiTamoom.DataLayer.Entities.Permission;
using HamechiTamoom.DataLayer.Entities.User;

namespace HamechiTamoom.Core.Services.Interfaces
{
    public interface IPermissionService
    {
        #region Roles

        List<Role> GetAllRoles();

        void AddRolesToUser(List<int> roleIds, int userId);

        void EditRolesUser(List<int> rolesId, int userId);

        void DeleteRole(Role role);

        int AddRole(Role role);

        Role GetRoleByRoleId(int roleId);

        void UpdateRole(Role role);

        #endregion

        #region Permissions

        List<Permission> GetAllPermissions();

        void AddPermissionsToRole(int roleId, List<int> permission);

        List<int> PermissionsRole(int roleId);

        void EditPermissionsRole(int roleId, List<int> permissions);

        bool CheckPermission(int permissionId, string userName);

        #endregion

        #region User

        public int GetUserIdByUserName(string userName);

        #endregion
    }
}
