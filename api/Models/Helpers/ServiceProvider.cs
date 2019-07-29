using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.Helpers
{
    public class ServiceProvider
    {
        public List<string> schemas { get; set; }
        public string patch { get; set; } = "1.0";
        public string bulk { get; set; } = "50";
        public string filter { get; set; } = "filter";
        public bool changePassword { get; set; } = false;
        public bool sort { get; set; } = false;
        public string etag { get; set; } = "myTag";

    }
}
