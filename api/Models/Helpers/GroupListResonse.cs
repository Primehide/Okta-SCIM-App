using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.Helpers
{
    public class GroupListResonse
    {
        public List<string> schemas { get; set; }
        public int totalResults { get; set; }
        public int itemsPerPage { get; set; }
        public int startIndex { get; set; }
        public List<Group> Resources { get; set; }

    }
}
