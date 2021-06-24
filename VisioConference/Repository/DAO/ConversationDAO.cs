using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VisioConference.Repository.Interface;
using VisioConference.Repository.Objets;

namespace VisioConference.Repository.DAO
{
    public class ConversationDAO : IConversation
    {
        // à modifier, le contexte doit être créé dans chaque méthode
        // Accès en ligne, il faut que les méthodes soient async et utiliser HasNotracking pour les histoire de cache OU utiliser le using Mycontext dans les méthodes
        //MyContext context = new MyContext();
        public List<Conversation> findAll()
        {
            using (MyContext context = new MyContext())
            {
                List<Conversation> lst = new List<Conversation>();

                //List<Utilisateur> utilisateurs = context.utilisateurs.Include(u => u.login).ToList();
                
                var query = context.conversations; // <=> SELECT * FROM conversation
                foreach (var item in query)
                {
                    lst.Add(item);
                }
                return lst;
            }
        }

        public Conversation findByUsers(User user1, User user2)
        {
            using (MyContext context = new MyContext())
            {
                throw new NotImplementedException();
            }
        }

        public List<User> findFriends(User user)
        {
            //Person magnus = new Person { FirstName = "Magnus", LastName = "Hedlund" };
            //Person terry = new Person { FirstName = "Terry", LastName = "Adams" };
            //Person charlotte = new Person { FirstName = "Charlotte", LastName = "Weiss" };
            //Person arlene = new Person { FirstName = "Arlene", LastName = "Huff" };
            //Person rui = new Person { FirstName = "Rui", LastName = "Raposo" };
            //Person phyllis = new Person { FirstName = "Phyllis", LastName = "Harris" };

            //Cat barley = new Cat { Name = "Barley", Owner = terry };
            //Cat boots = new Cat { Name = "Boots", Owner = terry };
            //Cat whiskers = new Cat { Name = "Whiskers", Owner = charlotte };
            //Cat bluemoon = new Cat { Name = "Blue Moon", Owner = rui };
            //Cat daisy = new Cat { Name = "Daisy", Owner = magnus };

            //Dog fourwheeldrive = new Dog { Name = "Four Wheel Drive", Owner = phyllis };
            //Dog duke = new Dog { Name = "Duke", Owner = magnus };
            //Dog denim = new Dog { Name = "Denim", Owner = terry };
            //Dog wiley = new Dog { Name = "Wiley", Owner = charlotte };
            //Dog snoopy = new Dog { Name = "Snoopy", Owner = rui };
            //Dog snickers = new Dog { Name = "Snickers", Owner = arlene };

            //// Create three lists.
            //List<Person> people =
            //    new List<Person> { magnus, terry, charlotte, arlene, rui, phyllis };
            //List<Cat> cats =
            //    new List<Cat> { barley, boots, whiskers, bluemoon, daisy };
            //List<Dog> dogs =
            //    new List<Dog> { fourwheeldrive, duke, denim, wiley, snoopy, snickers };

            //// The first join matches Person and Cat.Owner from the list of people and
            //// cats, based on a common Person. The second join matches dogs whose names start
            //// with the same letter as the cats that have the same owner.
            //var query = from person in people
            //            join cat in cats on person equals cat.Owner
            //            join dog in dogs on
            //            new { Owner = person, Letter = cat.Name.Substring(0, 1) }
            //            equals new { dog.Owner, Letter = dog.Name.Substring(0, 1) }
            //            select new { CatName = cat.Name, DogName = dog.Name };


            using (MyContext context = new MyContext())
            {
                var query = from us in context.users
                            join co in context.conversations on us.id equals co.userID
                            where co.userFriendID == user.id
                            //join co2 in context.conversations on us.id equals co2.userID
                            //where co2.userFriendID == user.id
                            select new
                            {
                                FriendId = us.id,
                                FriendLogin = us.login,
                                FriendConnected = us.connected
                            };
                var query2 = from us in context.users
                             join co in context.conversations on us.id equals co.userFriendID
                             where co.userID == user.id
                             //join co2 in context.conversations on us.id equals co2.userID
                             //where co2.userFriendID == user.id
                             select new
                             {
                                 FriendId = us.id,
                                 FriendLogin = us.login,
                                 FriendConnected = us.connected
                             };
                List<User> lst = new List<User>();
                foreach (var item in query)
                {
                    lst.Add(new User(item.FriendId, item.FriendLogin, item.FriendConnected));
                }
                foreach (var item in query2)
                {
                    lst.Add(new User(item.FriendId, item.FriendLogin, item.FriendConnected));
                }

                //from co in context.conversations
                //where (co.userFriendID == user.id || co.userID == user.id)
                //select new
                //{
                //    UserId = co.userID,
                //    userFriendID = co.userFriendID,
                //};

                return lst;
            }
        }
        /// <summary>
        /// Modifie la conversation (suite à envoi de message), si dépasse une certaine taille, on supprime les messages les plus anciens jusqu'à avoir la taille désirée
        /// </summary>
        /// <param name="u"></param>
        /// <param name="message"></param>
        public void modifyMessage(Conversation u, string message)
        {
            using (MyContext context = new MyContext())
            {
                throw new NotImplementedException();
            }
        }

        public void Update(Conversation u)
        {
            using (MyContext context = new MyContext())
            {
                throw new NotImplementedException();
            }
        }
    }
}