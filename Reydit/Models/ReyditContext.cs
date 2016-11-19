using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Reydit.Models
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class ReyditContext : DbContext
    {
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