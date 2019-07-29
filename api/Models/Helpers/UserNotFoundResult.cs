using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.Helpers
{
    public class UserNotFoundResult
    {
        public List<string> schemas { get; set; } = new List<string>()
        {
            "urn:ietf:params:scim:api:messages:2.0:Error"
        };
        public string detail { get; set; }
    }
}
