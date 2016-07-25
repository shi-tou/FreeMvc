using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yandex.Web.Models
{
    /// <summary>
    /// Ztree插件数据
    /// </summary>
    public class ZTreeData
    {
        public string id { get; set; }
        public string pId { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        [JsonProperty("checked")]
        public bool Checked { get; set; }
    }
}