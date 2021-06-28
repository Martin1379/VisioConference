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
        public List<User> findAll()
        {
            using (MyContext context = new MyContext())
            {
                List<User> lst = new List<User>();

                // <=> SELECT * FROM utilisateur
                var query = context.users;
                foreach (var item in query)
                {
                        lst.Add(item);
                }
                return lst;
            }
        }

        public List<User> findAllConnected()
        {
            using (MyContext context = new MyContext())
            {


                List<User> lst = new List<User>();

                //List<Utilisateur> utilisateurs = context.utilisateurs.Include(u => u.login).ToList();
                // <=> SELECT * FROM utilisateur
                var query = context.users;
                foreach (var item in query)
                {
                    if (item.Connected == true)
                    {
                        lst.Add(item);
                    }
                }
                return lst;
            }
        }

        public User findByLogin(string login)
        {
            using (MyContext context = new MyContext())
            {

                User user = new User();
                var query = from u in context.users
                            where u.Login == login
                            select u;
                user = query.FirstOrDefault();
                return user;
            }
        }

        public void Update()
        {
            using (MyContext context = new MyContext())
            {
                context.SaveChanges();
            }
        }
    }
}