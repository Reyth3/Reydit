﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Reydit.Models
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class ReyditContext : DbContext
    {
        private static ReyditContext current;

        public static ReyditContext Current
        {
            get { if (current == null) current = new ReyditContext(); else { current.Dispose(); current = new ReyditContext(); } return current; }
        }


        public ReyditContext()
        {
            Database.CreateIfNotExists();
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ReyditContext>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Subreydit> Subreydits { get; set; }
        public DbSet<Session> Sessions { get; set; }
    }
}