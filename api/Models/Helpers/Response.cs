using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.Helpers
{
    public class Response
    {
        public List<User> Resources { get; set; }
        public List<string> schemas { get; set; } = new List<string>()
        {
            "urn:ietf:params:scim:api:messages:2.0:ListResponse"
        };
        public int itemsPerPage { get; set; }
        public int startIndex { get; set; }
        public int totalResults { get; set; }
    }
}
