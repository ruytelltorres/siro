using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIRO.Models
{
    [Serializable]
    public class NodeModel
    {
        public string id { get; set; }
        public string icon { get; set; }
        public string text { get; set; }
        public int nodeId { get; set; }
        public List<NodeModel> nodes { get; set; }
    }
}