using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using api.EF;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using api.Models.Helpers;

namespace api.Controllers
{
    [Route("scim/V2/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SCIMContext _context;

        public UsersController()
        {
            _context = new SCIMContext();
        }

        [HttpPost]
        public ActionResult Post([FromBody]User user)
        {
            if(_context.Users.SingleOrDefault(x => x.userName == user.userName) != null)
            {

                return StatusCode(409);
            }
            else
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return StatusCode(201, user);
            }        
        }


        [HttpGet("{id}")]
        public ActionResult<User> Get(string id)
        {
            User user = _context.Users.Include(x => x.name).Include(x => x.emails).SingleOrDefault(x => x.id == id);
            if(user == null)
            {
                UserNotFoundResult userNotFoundResult = new UserNotFoundResult()
                {
                    detail = "Resource " + id + " not found"
                };
                return NotFound(userNotFoundResult);
            } else
            {
                return Ok(user);
            }
        }


        private Response RunFilter(string filter, int count, int startIndex)
        {
            string output = filter.Split("eq").Last();
            output = output.Replace("\"", "");
            output = output.Replace(" ", "");
            List<User> matchedUsers = _context.Users.Include(x => x.name).Include(x => x.emails).Where(x => x.userName.ToLower().Equals(output.ToLower())).ToList();
            Response result = new Response()
            {
                Resources = new List<User>(),
                totalResults = matchedUsers.Count(),
                itemsPerPage = count,
                startIndex = startIndex
            };
            result.Resources.AddRange(matchedUsers);
            return result;
        }

        private Response RunCount(int count, int startIndex)
        {
            Response result = new Response()
            {
                itemsPerPage = count,
                startIndex = startIndex,
                Resources = new List<User>()
            };
            if (startIndex > _context.Users.Count())
            {
                result.Resources = new List<User>();
                result.totalResults = 0;
                return result;
            }
            else
            {
                List<User> users = new List<User>();
                try
                {
                    users = _context.Users.Include(x => x.emails).Include(x => x.name).ToList().GetRange(startIndex - 1, count);
                } catch (System.ArgumentException)
                {
                    users = _context.Users.Include(x => x.emails).Include(x => x.name).ToList().GetRange(startIndex - 1, _context.Users.Count());
                }
                result.totalResults = users.Count();
                result.Resources = users;
            }
            return result;
        }

        [HttpGet]
        public ActionResult Get(int count = 999999, int startIndex = 999999, string filter="")
        {
            if(filter != "")
            {
                api.Models.Helpers.Response response = (RunFilter(filter, count, startIndex));
                string json = JsonConvert.SerializeObject(response);
                return Ok(json);
            }
            else
            {
                api.Models.Helpers.Response response = (RunCount(count, startIndex));
                string json = JsonConvert.SerializeObject(response);
                return Ok(json);
            }
        }

        [HttpPost]
        [Route("/scim/V2/[controller]/[action]")]
        public ActionResult CreateTestGroup()
        {
            Group group = new Group()
            {
                id = Guid.NewGuid().ToString(),
                externalId = "G1",
                displayName = "Test Group",
                groupType = GroupType.Organization,
                meta = new Meta()
                {
                    resourceType = resourceType.Group,
                    created = DateTime.Now,
                    lastModified = DateTime.Now,
                    location = "https://api.verschueren.me/api/user/GetGroups"
                },
            };
            _context.Groups.Add(group);
            _context.SaveChanges();
            return StatusCode(201);
        }

        [HttpPost]
        [Route("/scim/V2/[controller]/[action]")]
        public ActionResult CreateTestUser()
        {
            
            Models.User user = new User()
            {
                id = "2819c223-7f76-453a-919d-413861904646",
                externalId = "bjensen",
                userName = "bjensen",
                name = new Name()
                {
                    familyName = "bjergsens",
                    givenName = "bjensen bjergsens",
                },
                emails = new List<email>()
                {
                    new email()
                    {
                        value = "bjensen@example.com",
                        type = "work"
                    }
                },
                active = true
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return StatusCode(200);
        }

        [HttpPatch("{id}")]
        public ActionResult Patch(string id)
        {
            User user = _context.Users.Single(x => x.id == id);
            if (user.active)
            {
                user.active = false;
            }
            else
            {
                user.active = true;
            }
            
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return StatusCode(200,user);
        }

        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] User user)
        {
            User userToEdit = _context.Users.Include(x => x.name).Include(x => x.emails).Single(x => x.id == id);
            //updating relating entity
            //you can only do it the hacky way.
            userToEdit.name.givenName = user.name.givenName;
            userToEdit.name.familyName = user.name.familyName;
            foreach (var e in user.emails)
            {
                if(userToEdit.emails.SingleOrDefault(x => x.value == e.value) == null)
                {
                    userToEdit.emails.Add(e);
                }
            }

            _context.Entry(userToEdit).State = EntityState.Modified;
            _context.SaveChanges();
            return StatusCode(200, user);
        }
    }
}