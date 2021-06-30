using System;
using System.Data.Entity;
using System.Linq;
using VisioConference.Repository.Objets;

namespace VisioConference.Repository.DAO
{
    public class MyContext : DbContext
    {

        public MyContext()
            : base(/*name=*/"MyContext")
        {
        }

        public virtual DbSet<User> users { get; set; }
        public virtual DbSet<Conversation> conversations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .Property(e => e.Pseudo)
            .IsUnicode(false);

            //  modelBuilder.Entity<Conversation>()
            //.Property(e => e.userID)
            //.IsUnicode(false);

            //modelBuilder.Entity<Conversation>()
            //   .Property(e => e.userFriendID)
            //.IsUnicode(false);




        }
    }

}