using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Meta
    {
        public int metaId { get; set; }
        public resourceType resourceType { get; set; }
        public DateTime created { get; set; }
        public DateTime lastModified { get; set; }
        public string location { get; set; }
    }
}
