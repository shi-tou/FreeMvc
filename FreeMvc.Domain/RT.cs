using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeMvc.Domain
{
    public class RT
    {
        /// <summary>
        /// 成功
        /// </summary>
        public static int Success = 0;
        /// <summary>
        /// 失败
        /// </summary>
        public static int Failed = -1;
        /// <summary>
        /// 用户名不存在
        /// </summary>
        public static int User_NotExist_UserName = 10001;
        /// <summary>
        /// 登录密码错误
        /// </summary>
        public static int User_Error_Password = 10002;
    }
}
