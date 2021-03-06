using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamechiTamoom.DataLayer.Entities.Course;
using HamechiTamoom.DataLayer.Entities.Permission;
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

        #region Permission

        public DbSet<Permission> Permission { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }

        #endregion

        #region Course

        public DbSet<CourseGroup> CourseGroups { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseEpisode> CourseEpisodes { get; set; }
        public DbSet<CourseLevel> CourseLevels { get; set; }
        public DbSet<CourseStatus> CourseStatus { get; set; }
        public DbSet<nmn> Nmns { get; set; }
        public DbSet<AllCmnt> AllCmnts { get; set; }

        #endregion


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //    modelBuilder.Entity<AllCmnt>()
        //        .HasOne(e=>e.UserId)
        //        .WithMany(a=>a.)
        //    base.OnModelCreating(modelBuilder);
        //}

    }
}
