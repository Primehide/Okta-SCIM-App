using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using app.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;


namespace app.Services
{
    public class SimpleApiService : IApiService
    {
        private HttpClient client = new HttpClient();
        private readonly ITokenService tokenService;
        public SimpleApiService(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        public async Task<IList<string>> GetValues()
        {
            List<string> values = new List<string>();
            var token = tokenService.GetToken().Result;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var res = await client.GetAsync("https://api.verschueren.me/api/user");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                values = JsonConvert.DeserializeObject<List<string>>(json);
            }
            else
            {
                values = new List<string> { res.StatusCode.ToString(), res.ReasonPhrase };
            }
            return values;
        }

        public async Task<User> GetUser(string id)
        {
            User user = new User();
            var token = tokenService.GetToken().Result;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var res = await client.GetAsync("https://api.verschueren.me/api/user?id=" + id);
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<User>(json);
            }
            return user;
        }
    }
}