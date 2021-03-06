﻿using Reydit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reydit.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        /*public ActionResult Index()
        {
            return View();
        } */

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterResult()
        {
            var username = Request["username"];
            var email = Request["email"];
            var password = Request["password"];
            var confirm = Request["password2"];

            var rc = ReyditContext.Current;
            var success = false;
            try
            {
                if (password == confirm)
                {
                    rc.Users.Add(new Models.User(username, password, email));
                    rc.SaveChanges();
                    success = true;
                }
            }
            catch { success = false; }
            return View(success);
        }

        [HttpPost]
        public ActionResult LogIn()
        {
            var username = Request["username"];
            var password = Request["password"];

            Models.User.LogIn(username, password);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOut()
        {
            Models.User.LogOut();
            return RedirectToAction("Index", "Home");
        }

        [Route("u/{id}")]
        public ActionResult Profile(string id)
        {
            var user = new ReyditContext().Users.Where(o => o.Username.ToLower() == id.ToLower()).FirstOrDefault();
            return View(user); 
        }
    }
}