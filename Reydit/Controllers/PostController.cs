using Reydit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reydit.Controllers
{
    public class PostController : Controller
    {
        [Route("Post/Create/{type?}")]
        public ActionResult Create(string type)
        {
            if (type == null) type = "text";
            ViewBag.type = type.ToLower();
            return View();
        }

        [HttpPost]
        public ActionResult Process()
        {
            var user = Models.User.CurrentUser;
            if (user == null)
                return Content("You are not logged in!");
            var rc = ReyditContext.Current;
            var mode = Request["mode"];
            if(mode == "create")
            {
                var type = Request["type"];
                var sub = rc.Subreydits.Where(o => o.ShortName == Request["sub"]).FirstOrDefault();
                if (sub == null)
                    return Content("Specified subreydit does not exist!");
                var title = Request["title"];
                var content = "";
                var linkPost = false;
                if (type == "text")
                    content = Request["content"]; 
                else
                {
                    content = Request["url"];
                    linkPost = true;
                }
                var post = new Post(title, linkPost, content, user, sub);
                user.Posts.Add(post);
                sub.Posts.Add(post);
                rc.SaveChanges();
                return RedirectToAction("Show", new { id = post.Id });
            }

            return Content("An error occured!");
        }
    }
}