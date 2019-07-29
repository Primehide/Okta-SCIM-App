using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Group
    {
        [NotMapped]
        public List<string> schemas { get; set; }
        public string id { get; set; }
        public GroupType groupType { get; set; }
        public string externalId { get; set; }
        public string displayName { get; set; }
        public Meta meta { get; set; }
    }
}
