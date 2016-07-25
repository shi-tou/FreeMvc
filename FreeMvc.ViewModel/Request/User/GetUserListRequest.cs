using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreeMvc.ViewModel
{
    public class GetUserListRequest : BaseRequest
    {
        public string UserName { get; set; }
        public string Name { get; set; }
    }
}