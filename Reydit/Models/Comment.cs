﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reydit.Models
{
    public class Comment
    {
        public Comment() { }
        public Comment(string message, User author, Post post)
        {
            Message = message;
            Author = author;
            this.Post = post;
            PostingDate = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime PostingDate { get; set; }

        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        public int Score { get { return 1 + Upvotes - Downvotes; } }

        public virtual User Author { get; set; }
        public virtual Post Post { get; set; }
    }
}