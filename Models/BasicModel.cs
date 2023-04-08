using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class BasicModel
    {
        [JsonIgnore]
        [ScaffoldColumn(false)]
        public DateTime? CreatedOn { get; set; }
        [JsonIgnore]
        [ScaffoldColumn(false)]
        public DateTime? UpdatedOn { get; set; }
        [JsonIgnore]
        [ScaffoldColumn(false)]
        public string CreatedBy { get; set; }
        [JsonIgnore]
        [ScaffoldColumn(false)]
        public string UpdatedBy { get; set; }

    }
}