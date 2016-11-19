﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reydit.Models
{
    public class Post
    {
        public Post(string name, bool link, string content, User author)
        {
            Name = name;
            Link = link;
            Content = content;
            Author = author;
            PostingDate = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Link { get; set; }
        public string Content { get; set; }
        public DateTime PostingDate { get; set; }

        public virtual User Author { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}