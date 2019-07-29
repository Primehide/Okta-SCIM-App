using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string id { get; set; }
        public string externalId { get; set; }
        public string userName { get; set; }
        public Name name { get; set; }
        public bool active { get; set; }
        public List<email> emails { get; set; }
        [NotMapped]
        public List<string> schemas { get; set; } = new List<string>()
        {
            "urn:ietf:params:scim:schemas:core:2.0:User"
        };
    }
}
