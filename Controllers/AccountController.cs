using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers
{
    public class AccountController : Controller
    {
        private MyContext dbContext;
        public AccountController(MyContext context)
        {
            dbContext = context;
        }

        [HttpPost]
        public IActionResult Login(LogUser user)
        {
            if(ModelState.IsValid)
            {
                // if pass model validation specifications
                User loggedUser = dbContext.AllUsers
                    .FirstOrDefault(u => u.Email == user.LogEmail);
                // if user exits in the database
                if(loggedUser == null)
                {
                    ModelState.AddModelError("LogEmail", "Invalid Email!");
                    return View("~/Views/Home/index.cshtml");
                }
                // verify password of the specified login email
                var hasher = new PasswordHasher<LogUser>() ;
                var validation = hasher.VerifyHashedPassword(user, loggedUser.Password, user.LogPassword);
                if (validation == 0)
                {
                    ModelState.AddModelError("LogPassword", "Invalid Password!");
                    return View("~/Views/Home/index.cshtml");
                }
                // set user login information in session
                HttpContext.Session.SetInt32("User", loggedUser.UserId);
                return RedirectToAction("Dashboard", "Wedding");
            } 
            else 
            {
                // model state not valid render view with validation errors
                return View("~/Views/Home/index.cshtml");
            }
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if(ModelState.IsValid)
            {
                // if pass model validation check database for unique email
                if(dbContext.AllUsers.Any(u => u.Email == user.Email))
                {
                    // Manually add a ModelState error to the Email field, with provided
                    // error message of Email is not unique
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("~/Views/Home/index.cshtml");
                } 
                else 
                {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    user.Password = Hasher.HashPassword(user, user.Password);
                    dbContext.Add(user);
                    dbContext.SaveChanges();
                    // query through db again for user to set in session
                    HttpContext.Session.SetInt32("User", user.UserId);
                    return RedirectToAction("Dashboard", "Wedding");
                }
            } 
            else 
            {
                return View("~/Views/Home/index.cshtml");
            }
        }

        [HttpGet]
        public RedirectToActionResult Logout()
        {
            if(HttpContext.Session.GetInt32("User")==null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Home");
            }
        }
    }
}