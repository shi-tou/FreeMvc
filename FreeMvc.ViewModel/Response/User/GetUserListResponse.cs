using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeMvc.ViewModel
{
    public class GetUserListResponse
    {
        public string ID { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string RoleID { get; set; }

        public string RoleName { get; set; }

        public string CreateName { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
