using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Reydit.Models
{
    public class Session
    {
        public Session() { }
        public Session(User user)
        {
            Owner = user;
            Token = Guid.NewGuid().ToString();
            CreationDate = DateTime.UtcNow;
            LastRequested = new DateTime();
        }
        public int Id { get; set; }
        [MaxLength(255), Index(IsUnique = true)]
        public string Token { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastRequested { get; set; }

        public virtual User Owner { get; set; }
    }
}