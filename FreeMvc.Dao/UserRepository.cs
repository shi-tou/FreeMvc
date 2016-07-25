using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webdiyer.WebControls.Mvc;
using FreeMvc.Common;
using FreeMvc.Domain;
using FreeMvc.ViewModel;
using System.Data;

namespace FreeMvc.Dao
{
    public class UserRepository : BaseRepository
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PagedList<GetUserListResponse> GetUserList(GetUserListRequest request)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append(@"select a.*,b.Name as RoleName,c.Name as CreateName from T_User a
                            left join T_Role b on b.ID=a.RoleID
                            left join T_User c on c.ID=a.CreateBy
                            where 1=1 ");
            List<MySqlParameter> param = new List<MySqlParameter>();

            if (!string.IsNullOrEmpty(request.UserName))
            {
                sbSql.Append(" and a.UserName like ?UserName");
                param.Add(new MySqlParameter("UserName", "%" + request.UserName + "%"));
            }
            if (!string.IsNullOrEmpty(request.Name))
            {
                sbSql.Append(" and a.Name like ?Name");
                param.Add(new MySqlParameter("Name", "%" + request.Name + "%"));
            }
            sbSql.Append(" order by a.CreateTime desc");
            return GetPageList<GetUserListResponse>(sbSql.ToString(), param.ToArray(), request.PageIndex, request.PageSize,request.OrderBy);
        }
        /// <summary>
        /// 角色列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PagedList<RoleInfo> GetRoleList(GetRoleListRequest request)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append(@"select * from T_Role where 1=1");
            List<MySqlParameter> param = new List<MySqlParameter>();
            if (!string.IsNullOrEmpty(request.Name))
            {
                sbSql.Append(" and Name like ?Name");
                param.Add(new MySqlParameter("Name", "%" + request.Name + "%"));
            }
            return GetPageList<RoleInfo>(sbSql.ToString(), param.ToArray(), request.PageIndex, request.PageSize, request.OrderBy);
        }
        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<PermissionInfo> GetUserPermission(string userID)
        {
            StringBuilder sbSql = new StringBuilder();
            List<MySqlParameter> param = new List<MySqlParameter>();
            sbSql.Append(@"select c.* from T_RolePermission a
                            inner join T_User b on b.RoleID=a.RoleID
                            inner join T_Permission c on c.ID=a.PermissionID
                            where b.ID=@UserID");
            param.Add(new MySqlParameter("UserID", userID));
            return GetList<PermissionInfo>(sbSql.ToString(), param.ToArray(), CommandType.Text);
        }
    }
}
