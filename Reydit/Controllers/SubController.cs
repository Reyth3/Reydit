using Reydit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reydit.Controllers
{
    public class SubController : Controller
    {
        // GET: Sub
        [Route("subreydits/{sort?}")]
        public ActionResult Index(string sort)
        {
            var subs = new ReyditContext().Subreydits.ToList();
            return View(subs);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Process()
        {
            var shortName = Request["shortName"];
            var name = Request["name"];
            var desc = Request["desc"];
            var mode = Request["mode"];

            var rc = new ReyditContext();
            if(mode == "create")
            {
                var user = Models.User.CurrentUser;
                if (user == null)
                    return Content("<p>You are not logged in!</p>", "text/html");
                rc.Subreydits.Add(new Subreydit(name, desc, user) { ShortName = shortName });
                rc.SaveChanges();
                return RedirectToAction("Show", new { name = "" });
            }


            return Content("There was an error with the request.");
        }

    }
}