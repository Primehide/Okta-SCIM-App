using app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Services
{
    public interface IApiService
    {
        Task<IList<string>> GetValues();
        Task<User> GetUser(string id);
    }
}
