using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webdiyer.WebControls.Mvc;
using FreeMvc.Common;
using FreeMvc.Dao;
using FreeMvc.Domain;
using FreeMvc.ViewModel;

namespace FreeMvc.Service
{
    public class UserService : BaseService, IUserService
    {
        public UserRepository UserRepository;
        public UserService()
        {
            UserRepository = new UserRepository();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int UserLogin(string userName, string password, out UserInfo info)
        {
            info = GetModel<UserInfo>("UserName", userName);
            if (info == null)
                return RT.User_NotExist_UserName;
            if (info.Password != DEncrypt.Encrypt(password))
                return RT.User_Error_Password;
            return RT.Success;
        }
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public PagedList<GetUserListResponse> GetUserList(GetUserListRequest request)
        {
            return UserRepository.GetUserList(request);
        }
        /// <summary>
        /// 角色列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public PagedList<RoleInfo> GetRoleList(GetRoleListRequest request)
        {
            return UserRepository.GetRoleList(request);
        }
        /// <summary>
        /// 获取用户的权限
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<PermissionInfo> GetUserPermission(string userID)
        {
            return UserRepository.GetUserPermission(userID);
        }
    }
}
