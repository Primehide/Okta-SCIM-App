using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class email
    {
        public int EmailId { get; set; }
        public string value { get; set; }
        public string type { get; set; }
        public bool primary { get; set; }
    }
}
