using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.EF;
using Microsoft.EntityFrameworkCore;
using api.Models;

namespace WebUI.Controllers
{
    public class DemoController : Controller
    {

        private readonly SCIMContext _context;

        public DemoController()
        {
            _context = new SCIMContext();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Users()
        {
            List<api.Models.User> users = _context.Users.Include(x => x.name).Include(x => x.emails).ToList();
            return View(users);
        }

        public IActionResult Details(string id)
        {
            User user = _context.Users.Include(x => x.name).Include(x => x.emails).Single(x => x.id == id);
            return View(user);
        }
    }
}