using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Models
{
    public class User
    {
        public string id { get; set; }
        public string externalId { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string familyName { get; set; }
        public string givenName { get; set; }
        public string active { get; set; }
        public List<email> emails { get; set; }
    }
}
