using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;
using FreeMvc.Common;
using FreeMvc.Domain;
using FreeMvc.Service;
using FreeMvc.ViewModel;
using FreeMvc.Web.Attribute;
using Yandex.Web.Models;

namespace FreeMvc.Web.Controllers
{
    /// <summary>
    /// 用户模块
    /// </summary>
    [PermissionAttribute]
    public class UserController : BaseController
    {
        #region 登录相关
        [Anonymous]
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        [Anonymous]
        [HttpPost]
        public JsonResult Login(UserLoginRequest model)
        {
            UserInfo info;
            int res = UserService.UserLogin(model.UserName, model.Password, out info);
            if (res == RT.Success)
            {
                LoginUser.Current = info;
            }
            else if (res == RT.User_NotExist_UserName)
            {
                Result.IsOk = false;
                Result.Msg = "用户名不存在";
            }
            else if (res == RT.User_Error_Password)
            {
                Result.IsOk = false;
                Result.Msg = "用户名不存在";
            }
            return Json(Result);
        }
        /// <summary>
        /// 用户退出登录
        /// </summary>
        [OnlyNeedLogin]
        public ActionResult LoginOut()
        {
            //退出登录
            LoginUser.Current = null;
            return RedirectToAction("Login", "User");
        }
        #endregion

        #region 用户管理
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult UserList(GetUserListRequest request)
        {
            PagedList<GetUserListResponse> list = UserService.GetUserList(request);
            return View(list);
        }
        /// <summary>
        /// 用户添加
        /// </summary>
        /// <returns></returns>
        public ActionResult UserAdd(string id)
        {
            ViewData["Roles"] = new SelectList(UserService.GetList<RoleInfo>(), "ID", "Name");
            UserInfo info = UserService.GetModel<UserInfo>("ID", id);
            return View(info ?? new UserInfo());
        }
        /// <summary>
        /// 用户添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserAdd(UserInfo info)
        {
            if (string.IsNullOrEmpty(info.ID))
            {
                info.ID = GuidHelper.GetUniqueID();
                info.Password = DEncrypt.Encrypt(info.Password);
                if (UserService.Insert<UserInfo>(info))
                {
                    Result.IsOk = true;
                    Result.Msg = "添加成功";
                }
                else
                {
                    Result.IsOk = false;
                    Result.Msg = "添加失败";
                }
            }
            else
            {
                if (UserService.Update<UserInfo>(info))
                {
                    Result.IsOk = true;
                    Result.Msg = "更新成功";
                }
                else
                {
                    Result.IsOk = false;
                    Result.Msg = "更新失败";
                }
            }
            Result.RedirectUrl = "/User/UserList";
            return Json(Result);
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult UserDelete(string id)
        {
            if (UserService.Delete<UserInfo>("ID", id))
            {
                Result.IsOk = true;
                Result.Msg = "删除成功";
            }
            else
            {
                Result.IsOk = false;
                Result.Msg = "删除失败";
            }
            Result.RedirectUrl = "/User/UserList";
            return Json(Result);
        }
        #endregion

        #region 角色管理
        /// <summary>
        /// 角色列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult RoleList(GetRoleListRequest request)
        {
            PagedList<RoleInfo> list = UserService.GetRoleList(request);
            return View(list);
        }
        /// <summary>
        /// 角色添加
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleAdd(string id)
        {
            RoleInfo info = UserService.GetModel<RoleInfo>("ID", id);
            var permission = UserService.GetList<PermissionInfo>();
            var rolePermission = UserService.GetList<RolePermissionInfo>("RoleID",id);
            var ztree = from p in permission
                        select new ZTreeData
                        {
                            id = p.ID,
                            pId = p.ParentID,
                            name = p.Name,
                            value = p.ID,
                            Checked = rolePermission.Exists(a => a.PermissionID == p.ID)
                        };
            ViewBag.PermissionList = JsonConvert.SerializeObject(ztree);
            ViewBag.PermissionIDs = GetRolePermissionIDs(rolePermission);
            return View(info ?? new RoleInfo());
        }
        /// <summary>
        /// 角色添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RoleAdd(RoleInfo info, FormCollection form)
        {
            if (string.IsNullOrEmpty(info.ID))
            {
                info.ID = GuidHelper.GetUniqueID();
                if (UserService.Insert<RoleInfo>(info))
                {
                    string permissionIDs = form["PermissionIDs"];
                    UserService.Insert<RolePermissionInfo>(GetRolePermissionList(info.ID, permissionIDs));
                    Result.IsOk = true;
                    Result.Msg = "添加成功";
                }
                else
                {
                    Result.IsOk = false;
                    Result.Msg = "添加失败";
                }
            }
            else
            {
                if (UserService.Update<RoleInfo>(info))
                {
                    string permissionIDs = form["PermissionIDs"];
                    UserService.Delete<RolePermissionInfo>("RoleID", info.ID);
                    UserService.Insert<RolePermissionInfo>(GetRolePermissionList(info.ID, permissionIDs));
                    Result.IsOk = true;
                    Result.Msg = "更新成功";
                }
                else
                {
                    Result.IsOk = false;
                    Result.Msg = "更新失败";
                }
            }
            Result.RedirectUrl = "/User/RoleList";
            return Json(Result);
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult RoleDelete(string id)
        {
            if (UserService.Delete<RoleInfo>("ID", id))
            {
                Result.IsOk = true;
                Result.Msg = "删除成功";
            }
            else
            {
                Result.IsOk = false;
                Result.Msg = "删除失败";
            }
            return Json(Result);
        }
        /// <summary>
        /// 获取角色权限List
        /// </summary>
        /// <returns></returns>
        public List<RolePermissionInfo> GetRolePermissionList(string roleID, string permissionIDs)
        {
            List<RolePermissionInfo> list = new List<RolePermissionInfo>();
            foreach (string id in permissionIDs.Split(",".ToArray()))
            {
                if (string.IsNullOrEmpty(id))
                    continue;
                list.Add(new RolePermissionInfo { RoleID = roleID, PermissionID = id });
            }
            return list;
        }
        /// <summary>
        /// 构造角色权限ID集
        /// </summary>
        /// <returns></returns>
        public string GetRolePermissionIDs(List<RolePermissionInfo> list)
        {
            string permissinIDs = "";
            foreach (RolePermissionInfo info in list)
            {
                if (permissinIDs != "")
                    permissinIDs += ",";
                permissinIDs += info.PermissionID;
            }
            return permissinIDs;
        }

        #endregion

        #region 权限管理
        /// <summary>
        /// 权限列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult PermissionList()
        {
            return View(GetPermissionList());
        }
        /// <summary>
        /// 权限添加
        /// </summary>
        /// <returns></returns>
        public ActionResult PermissionAdd(string id)
        {
            PermissionInfo info = UserService.GetModel<PermissionInfo>("ID", id) ?? new PermissionInfo();
            if (info.Type == 0)
                info.Type = 1;
            ViewData["PermissionType"] = GetPermissionType(info);
            ViewData["PermissionList"] = new SelectList(from p in GetPermissionList() where p.Type != 3 select p, "ID", "Name");
            return View(info);
        }
        /// <summary>
        /// 权限添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PermissionAdd(PermissionInfo info)
        {
            info.ParentID = (string.IsNullOrEmpty(info.ParentID) ? "0" : info.ParentID);
            if (string.IsNullOrEmpty(info.ID))
            {
                PermissionInfo hasInfo = UserService.GetModel<PermissionInfo>("Code", info.Code);
                if (hasInfo != null && hasInfo.ID != "")
                {
                    Result.IsOk = false;
                    Result.Msg = "权限编码已存在";
                }
                else
                {
                    info.ID = GuidHelper.GetUniqueID(); 
                    if (UserService.Insert<PermissionInfo>(info))
                    {
                        Result.IsOk = true;
                        Result.Msg = "添加成功";
                    }
                    else
                    {
                        Result.IsOk = false;
                        Result.Msg = "添加失败";
                    }
                }
            }
            else
            {
                if (UserService.Update<PermissionInfo>(info))
                {
                    Result.IsOk = true;
                    Result.Msg = "更新成功";
                }
                else
                {
                    Result.IsOk = false;
                    Result.Msg = "更新失败";
                }
            }
            Result.RedirectUrl = "/User/PermissionList";
            return Json(Result);
        }
        /// <summary>
        /// 权限角色
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult PermissionDelete(string id)
        {
            if (UserService.Delete<PermissionInfo>("ID", id))
            {
                Result.IsOk = true;
                Result.Msg = "删除成功";
            }
            else
            {
                Result.IsOk = false;
                Result.Msg = "删除失败";
            }
            return Json(Result);
        }
        /// <summary>
        /// 构造权限类别下拉框数据
        /// </summary>
        /// <returns></returns>
        public SelectList GetPermissionType(PermissionInfo info)
        {
            List<PermissionType> list = new List<PermissionType>();
            list.Add(new PermissionType { ID = 1, Name = "模块"});
            list.Add(new PermissionType { ID = 2, Name = "主窗体" });
            list.Add(new PermissionType { ID = 3, Name = "工具栏" });
            return new SelectList(list, "ID", "Name");
        }
        /// <summary>
        /// 构造权限下拉框数据
        /// </summary>
        /// <returns></returns>
        public List<PermissionInfo> GetPermissionList()
        {
            List<PermissionInfo> permission = UserService.GetList<PermissionInfo>();
            List<PermissionInfo> permissionTemp = new List<PermissionInfo>();
            
            var listP1 = from p in permission
                         where p.Type == 1
                         orderby p.Sort
                         select p;
            foreach (var item1 in listP1)
            {
                permissionTemp.Add(item1);
                var listP2 = from p in permission
                          where p.Type == 2 && p.ParentID == item1.ID
                          orderby p.Sort
                          select p;
                foreach (var item2 in listP2)
                {
                    item2.Name = "|----" + item2.Name;
                    permissionTemp.Add(item2);
                    var listP3 = from p in permission
                                 where p.Type == 3 && p.ParentID == item2.ID
                                 orderby p.Sort
                                 select p;
                    foreach (var item3 in listP3)
                    {
                        item3.Name = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|----" + item3.Name;
                        permissionTemp.Add(item3);
                    }
                }
            }
            return permissionTemp;
        }
        #endregion

        
    }
}