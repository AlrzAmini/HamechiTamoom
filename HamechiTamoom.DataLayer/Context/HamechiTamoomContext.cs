using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamechiTamoom.DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace HamechiTamoom.DataLayer.Context
{
    public class HamechiTamoomContext : DbContext
    {
        public HamechiTamoomContext(DbContextOptions<HamechiTamoomContext> options)
        :base(options)
        {

        }

        #region User

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        #endregion

    }
}
