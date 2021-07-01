using System;
using System.Data.Entity;
using System.Linq;
using VisioConference.Models;

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
        //A laisser, créé lors en même temps que le UserController
        public System.Data.Entity.DbSet<VisioConference.DTO.UserDTO> UserDTOes { get; set; }
    }

}