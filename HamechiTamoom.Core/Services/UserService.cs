using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamechiTamoom.Core.Convertors;
using HamechiTamoom.Core.DTOs;
using HamechiTamoom.Core.Generator;
using HamechiTamoom.Core.Security;
using HamechiTamoom.Core.Services.Interfaces;
using HamechiTamoom.DataLayer.Context;
using HamechiTamoom.DataLayer.Entities.User;

namespace HamechiTamoom.Core.Services
{
    public class UserService : IUserService
    {
        private readonly HamechiTamoomContext _context;

        public UserService(HamechiTamoomContext context)
        {
            _context = context;
        }

        #region Queries

        public bool IsExistUserName(string userName)
        {
            return _context.Users.Any(u => u.UserName == userName);
        }

        public bool IsExistEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }

        public User LoginUser(LoginViewModel login)
        {
            string hashedPassword = PasswordHelper.EncodePasswordMd5(login.Password);
            string email = FixText.FixEmail(login.Email);
            return _context.Users.SingleOrDefault(u => u.Email == email && u.Password == hashedPassword);
        }

        public bool ActiveAccount(string activeCode)
        {
            var user = _context.Users.SingleOrDefault(u => u.ActivationCode == activeCode);
            if (user == null || user.IsActive)
            {
                return false;
            }

            user.IsActive = true;
            user.ActivationCode = CodeGenerator.GenerateUniqCode();
            _context.SaveChanges();

            return true;
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }

        public User GetUserByActiveCode(string activeCode)
        {
            return _context.Users.SingleOrDefault(u => u.ActivationCode == activeCode);
        }

        public void UpdateUser(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }

        public InformationUserViewModel GetUserInformation(string userName)
        {
            User user = GetUserByUserName(userName);

            InformationUserViewModel information = new InformationUserViewModel();
            information.UserName = user.UserName;
            information.Email = user.Email;
            information.RegisterDate = user.RegisterDate;
            information.Wallet = 0;
            information.UserAvatar = user.UserAvatar;

            return information;
        }

        public InformationUserViewModel GetUserInformation(int userId)
        {
            User user = GetUserByUserId(userId);

            InformationUserViewModel information = new InformationUserViewModel();
            information.UserName = user.UserName;
            information.Email = user.Email;
            information.RegisterDate = user.RegisterDate;
            information.Wallet = 0;

            return information;
        }

        public User GetUserByUserName(string userName)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == userName);
        }

        public SideBarUserPanelViewModel GetSideBarUserPanelData(string userName)
        {
            return _context.Users.Where(u => u.UserName == userName).Select(u => new SideBarUserPanelViewModel()
            {
                UserName = u.UserName,
                ImageName = u.UserAvatar
            }).Single();
        }

        public EditUserProfileViewModel GetUserDataForEditProfile(string userName)
        {
            return _context.Users.Where(u => u.UserName == userName).Select(u => new EditUserProfileViewModel()
            {
                UserName = u.UserName,
                CurrentAvatar = u.UserAvatar
            }).Single();
        }

        public void EditProfile(string userName, EditUserProfileViewModel profile)
        {
            if (profile.UserAvatar != null)
            {
                string imgPath = "";
                // if new image was exist
                if (profile.CurrentAvatar != "Avatar-min.jpg")
                {
                    imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/UserAvatar",
                        profile.CurrentAvatar);
                    if (File.Exists(imgPath))
                    {
                        File.Delete(imgPath);
                    }
                }

                profile.CurrentAvatar = CodeGenerator.GenerateUniqCode() + Path.GetExtension(profile.UserAvatar.FileName);

                imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/UserAvatar",
                    profile.CurrentAvatar);
                using (var stream = new FileStream(imgPath, FileMode.Create))
                {
                    profile.UserAvatar.CopyTo(stream);
                }
            }

            User user = GetUserByUserName(userName);
            user.UserName = profile.UserName;
            user.UserAvatar = profile.CurrentAvatar;

            UpdateUser(user);

        }

        public bool CompareOldPassword(string oldPassword, string username)
        {
            string hashOldPassword = PasswordHelper.EncodePasswordMd5(oldPassword);
            return _context.Users.Any(u => u.UserName == username && u.Password == hashOldPassword);
        }

        public void ChangePassword(string userName, string newPassword)
        {
            User user = GetUserByUserName(userName);
            user.Password = PasswordHelper.EncodePasswordMd5(newPassword);
            UpdateUser(user);
        }

        public UsersForAdminViewModel GetUsersByFilter(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {
            IQueryable<User> result = _context.Users;

            // filter list by email
            if (!string.IsNullOrEmpty(filterEmail))
            {
                result = result.Where(u => u.Email.Contains(filterEmail));
            }

            // filter list by username
            if (!string.IsNullOrEmpty(filterUserName))
            {
                result = result.Where(u => u.UserName.Contains(filterUserName));
            }

            // show item in page 
            int take = 10;
            int skip = (pageId - 1) * take;

            // Paging and Users
            UsersForAdminViewModel lstUsers = new UsersForAdminViewModel();
            lstUsers.CurrentPage = pageId;
            lstUsers.TotalPage = (int)Math.Ceiling((decimal)result.Count() / take);
            lstUsers.Users = result.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList();

            return lstUsers;
        }

        public int AddUserFromAdmin(CreateUserViewModel user)
        {
            User addUser = new User();
            addUser.UserName = user.UserName;
            addUser.Email = user.Email;
            addUser.Password = PasswordHelper.EncodePasswordMd5(user.Password);
            addUser.RegisterDate = DateTime.Now;
            addUser.ActivationCode = CodeGenerator.GenerateUniqCode();
            addUser.IsActive = true;
            #region Save User Avatar

            if (user.UserAvatar != null)
            {
                string imgPath = "";

                addUser.UserAvatar = CodeGenerator.GenerateUniqCode() + Path.GetExtension(user.UserAvatar.FileName);

                imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/UserAvatar",
                    addUser.UserAvatar);
                using (var stream = new FileStream(imgPath, FileMode.Create))
                {
                    user.UserAvatar.CopyTo(stream);
                }
            }

            #endregion

            return AddUser(addUser);
        }

        public EditUserViewModel GetUserForShowInEditMode(int userId)
        {
            return _context.Users.Where(u => u.UserId == userId)
                .Select(u => new EditUserViewModel()
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Email = u.Email,
                    AvatarName = u.UserAvatar,
                    UserRoles = u.UserRoles.Select(r => r.RoleId).ToList()

                }).Single();
        }

        public User GetUserByUserId(int userId)
        {
            return _context.Users.SingleOrDefault(u => u.UserId == userId);
        }

        public void EditUserFromAdmin(EditUserViewModel editUser)
        {
            User user = GetUserByUserId(editUser.UserId);
            //user.UserName = user name cant change ...
            user.Email = editUser.Email;

            if (!string.IsNullOrEmpty(editUser.Password))
            {
                user.Password = PasswordHelper.EncodePasswordMd5(editUser.Password);
            }


            if (editUser.UserAvatar != null)
            {

                // Delete Old Image
                if (editUser.AvatarName != "Avatar-min.jpg")
                {
                    string deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/UserAvatar",
                        editUser.AvatarName);
                    if (File.Exists(deletePath))
                    {
                        File.Delete(deletePath);
                    }
                }

                // Save New Image
                user.UserAvatar = CodeGenerator.GenerateUniqCode() + Path.GetExtension(editUser.UserAvatar.FileName);

                string imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/UserAvatar",
                    user.UserAvatar);
                using (var stream = new FileStream(imgPath, FileMode.Create))
                {
                    editUser.UserAvatar.CopyTo(stream);
                }
            }

            UpdateUser(user);
        }

        public void DeleteUserFromAdmin(int userId)
        {
            User user = GetUserByUserId(userId);

            _context.Remove(user);
            _context.SaveChanges();
        }


        #endregion

    }
}
