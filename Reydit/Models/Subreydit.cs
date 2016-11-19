using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reydit.Models
{
    public class Subreydit
    {
        public Subreydit(string name, string desc, User owner)
        {
            Name = name;
            Description = desc;
            CreationDate = DateTime.UtcNow;
            Owner = owner;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual User Owner { get; set; }
        public virtual List<User> Subscribers { get; set; }
        public virtual List<Post> Posts { get; set; }
    }
}