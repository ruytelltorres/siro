using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIRO.Models
{
    public class ReportGrafModel
    {

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string title { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string subtitle { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<object> data { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<object> drilldown { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string xkey { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ykeys { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> labels { get; set; }

        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        //public List<string> barColors { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int interval { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int ymax { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int ymin { get; set; }

        public List<string> colors { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> categories { get; set; }

        public List<dynamic> dataReport { get; set; }

  
        
    }
}