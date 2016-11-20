using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Reydit.Models
{
    public class Session
    {
        public Session(User user)
        {
            Owner = user;
            Token = new Guid().ToString();
            CreationDate = DateTime.UtcNow;
            LastRequested = new DateTime();
        }
        public int Id { get; set; }
        [Index(IsUnique = true)]
        public string Token { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastRequested { get; set; }

        public virtual User Owner { get; set; }
    }
}