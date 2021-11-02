﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamechiTamoom.Core.Services.Interfaces;
using HamechiTamoom.DataLayer.Context;
using HamechiTamoom.DataLayer.Entities.User;

namespace HamechiTamoom.Core.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly HamechiTamoomContext _context;

        public PermissionService(HamechiTamoomContext context)
        {
            _context = context;
        }

        public List<Role> GetAllRoles()
        {
            return _context.Roles.ToList();
        }

        public void AddRolesToUser(List<int> roleIds, int userId)
        {
            foreach (int roleId in roleIds)
            {
                _context.UserRoles.Add(new UserRole()
                {
                    RoleId = roleId,
                    UserId = userId
                });

            }

            _context.SaveChanges();
        }

        public void EditRolesUser(List<int> rolesId, int userId)
        {

            #region Delete User Roles

            _context.UserRoles.Where(u => u.UserId == userId).ToList().ForEach(r => _context.UserRoles.Remove(r));

            #endregion

            #region Add New Roles

            AddRolesToUser(rolesId,userId);

            #endregion

            _context.SaveChanges();
        }

        public int AddRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();

            return role.RoleId;
        }

        public Role GetRoleByRoleId(int roleId)
        {
           return _context.Roles.Find(roleId);
        }

        public void UpdateRole(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
        }

        public void DeleteRole(Role role)
        {
            _context.Roles.Remove(role);
            _context.SaveChanges();
        }
    }
}
