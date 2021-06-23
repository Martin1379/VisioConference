using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VisioConference.Repository.Interface;
using VisioConference.Repository.Objets;

namespace VisioConference.Repository.DAO
{
    public class UserDAO : IUser
    {
        MyContext context = new MyContext();
        public List<User> findAll()
        {
            List<User> lst = new List<User>();

            //List<Utilisateur> utilisateurs = context.utilisateurs.Include(u => u.login).ToList();
            // <=> SELECT * FROM utilisateur
            var query = context.users;
            foreach (var item in query)
            {
                if (item.connected == true)
                {
                    lst.Add(item);
                }
            }
            return lst;
        }

        public User findByLogin(string login)
        {
            User user = new User();
            var query = from u in context.users
                        where u.login == login
                        select u;
            user = query.FirstOrDefault();
            return user;
        }

        public void Update()
        {
            context.SaveChanges();
        }
    }
}