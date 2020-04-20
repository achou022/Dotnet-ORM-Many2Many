using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Models;
using WeddingPlanner.Models.ViewModels;

namespace WeddingPlanner.Controllers
{
    public class WeddingController : Controller
    {
        private MyContext dbContext;
        public WeddingController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            if(HttpContext.Session.GetInt32("User")==null)
            {
                return RedirectToAction("Index", "Home");
            }
            DashVM wrapper = new DashVM();
            wrapper.loggedUser = HttpContext.Session.GetInt32("User") ?? default(int);
            wrapper.AllWeddings = dbContext.AllWeddings
                .Include(aw => aw.RSVPs)
                .ToList();
            return View(wrapper);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            if(HttpContext.Session.GetInt32("User")==null)
            {
                return RedirectToAction("Index", "Home");
            }
            Wedding target = dbContext.AllWeddings
                .Include(w => w.RSVPs)
                .ThenInclude(rsvps => rsvps.Attendee)
                .FirstOrDefault(w => w.WeddingId == id);
            if(target == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(target);
        }

        [HttpGet]
        public IActionResult New()
        {
            if(HttpContext.Session.GetInt32("User")==null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(WeddingVM postData)
        {
            if(HttpContext.Session.GetInt32("User")==null)
            {
                return RedirectToAction("Index", "Home");
            }
            if(ModelState.IsValid)
            {
                int? loggedUser = HttpContext.Session.GetInt32("User");
                if(loggedUser != null)
                {
                    Wedding newWedding = new Wedding
                    {
                        Bride = postData.Bride,
                        Groom = postData.Groom,
                        Date = postData.Date,
                        Location = postData.Location,
                        CreatorId = (int)loggedUser
                    };
                    dbContext.Add(newWedding);
                    dbContext.SaveChanges();
                    return RedirectToAction("Dashboard", "Wedding");
                }
                return RedirectToAction("Index", "Home");
            }
            return View("New");
        }

        [HttpGet]
        public IActionResult DeleteWedding(int id)
        {
            if(HttpContext.Session.GetInt32("User")==null)
            {
                return RedirectToAction("Index", "Home");
            }
            Wedding target = dbContext.AllWeddings.FirstOrDefault(w => w.WeddingId == id);
            if(target == null)
            {
                return RedirectToAction("Index", "Home");
            }
            dbContext.AllWeddings.Remove(target);
            dbContext.SaveChanges();
            return RedirectToAction("DashBoard");
        }

        [HttpGet]
        public IActionResult AddRSVP(int id)
        {
            if(HttpContext.Session.GetInt32("User")==null)
            {
                return RedirectToAction("Index", "Home");
            }
            int AttendeeId = (int)HttpContext.Session.GetInt32("User");
            // Check if already RSVP
            if(dbContext.AllRSVPs.Any(rsvp => rsvp.AttendeeId == AttendeeId && rsvp.WeddingId == id))
            {
                return RedirectToAction("Index", "Home");
            }
            RSVP temp = new RSVP
            {
                WeddingId=id,
                AttendeeId=AttendeeId
            };
            dbContext.Add(temp);
            dbContext.SaveChanges();
            return RedirectToAction("DashBoard");
        }

        [HttpGet]
        public IActionResult RemoveRSVP(int id)
        {
            if(HttpContext.Session.GetInt32("User")==null)
            {
                return RedirectToAction("Index", "Home");
            }
            int attendee = (int)HttpContext.Session.GetInt32("User");
            RSVP target = dbContext.AllRSVPs
                .FirstOrDefault(rsvp => rsvp.AttendeeId == attendee && rsvp.WeddingId == id);
            // error if target doesn't exist
            if(target == null)
            {
                return RedirectToAction("Index", "Home");
            }
            dbContext.AllRSVPs.Remove(target);
            dbContext.SaveChanges();
            return RedirectToAction("DashBoard");
        }
    }
}