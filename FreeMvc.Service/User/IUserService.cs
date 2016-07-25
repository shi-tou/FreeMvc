using Webdiyer.WebControls.Mvc;
using FreeMvc.Common;
using FreeMvc.Domain;
using FreeMvc.ViewModel;
using System.Collections.Generic;

namespace FreeMvc.Service
{
    public interface IUserService : IBaseService
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        int UserLogin(string userName, string password, out UserInfo info);
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        PagedList<GetUserListResponse> GetUserList(GetUserListRequest request);
        /// <summary>
        /// 角色列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        PagedList<RoleInfo> GetRoleList(GetRoleListRequest request);
        /// <summary>
        /// 获取用户的权限
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        List<PermissionInfo> GetUserPermission(string userID);
    }
}
