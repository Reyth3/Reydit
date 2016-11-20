using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace Reydit.Models
{
    public class User
    {
        public User(string username, string password, string email)
        {
            Username = username;
            var hash = (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(password));
            Password = string.Join("", hash.Select(o => o.ToString("X2")));
            Email = email;
            RegistrationDate = DateTime.UtcNow;
            Posts = new List<Post>();
            Comments = new List<Comment>();
        }

        public int Id { get; set; }
        [Index(IsUnique = true)]
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }

        public virtual List<Post> Posts { get; set; }
        public virtual List<Comment> Comments { get; set; }

        public static bool LogIn(string username, string password)
        {
            var rc = new ReyditContext();
            var pwHash = string.Join("", (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(input)).Select(o => o.ToString("X２")));
            try
            {
                var match = rc.Users.FirstOrDefault(o => o.Username == username && o.Password == pwHash);
                if (match != null)
                {
                    var session = rc.Sessions.LastOrDefault(o => o.Owner == match);
                    if(session == null)
                    {
                        session = new Session(match);
                        rc.Sessions.Add(session);
                    }
                    session.LastRequested = DateTime.UtcNow;
                    HttpContext.Current.Response.Cookies.Add(new HttpCookie("user_token", session.Token));
                    return true;
                }
                else return false;
            }
            catch { return false; }
        }

        public static bool LogOut()
        {
            var cookie = HttpContext.Current.Response.Cookies.Get("user_token");
            if (cookie != null)
                HttpContext.Current.Response.Cookies.Remove("user_token");
            else return false;
            return true;
        }

        public static bool IsLoggedIn { get { return HttpContext.Current.Response.Cookies.Get("user_token") != null; } }

        public static User CurrentUser { get
            {
                var rc = new ReyditContext();
                var c = HttpContext.Current.Response.Cookies.Get("user_token");
                if (c != null)
                {
                    var session = rc.Sessions.LastOrDefault(o => o.Token == c.Value);
                    if (session != null)
                        return session.Owner;
                    LogOut();
                    return null;
                }
                else return null;
            }
        }
    }
}