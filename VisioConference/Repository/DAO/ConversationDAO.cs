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
        // Accès en ligne, il faut que les méthodes soient async et utiliser HasNotracking pour les histoire de cache
        // => OU utiliser le using Mycontext dans les méthodes
        public List<Conversation> findAll()
        {
            using (MyContext context = new MyContext())
            {
                List<Conversation> lst = new List<Conversation>();

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
                var query = from co in context.conversations
                            where (co.userFriendID == user1.id && co.userID == user2.id) || (co.userFriendID == user2.id && co.userID == user1.id)
                            select co;
                Conversation conv = query.FirstOrDefault(); //TODO try catch
                return conv;
            }
        }

        public List<User> findFriends(User user)
        {
            using (MyContext context = new MyContext())
            {
                var query = (from us in context.users
                             join co in context.conversations on us.id equals co.userID
                             where co.userFriendID == user.id
                             select new
                             {
                                 FriendId = us.id,
                                 FriendLogin = us.login,
                                 FriendConnected = us.connected
                             })
                            .Union
                            (from us in context.users
                             join co in context.conversations on us.id equals co.userFriendID
                             where co.userID == user.id
                             select new
                             {
                                 FriendId = us.id,
                                 FriendLogin = us.login,
                                 FriendConnected = us.connected
                             })
                            ;
                List<User> lst = new List<User>();
                foreach (var item in query)
                {
                    lst.Add(new User(item.FriendId, item.FriendLogin, item.FriendConnected));
                }
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
                var query = from co in context.conversations
                            where u.convID == co.convID
                            select co;
                query.FirstOrDefault().message = message;
                context.SaveChanges();            
            }
        }

        public void Update(Conversation u)
        {
            using (MyContext context = new MyContext())
            {
                var query = from co in context.conversations
                            where u.convID == co.convID
                            select co;
                query.FirstOrDefault().message = u.message;
                query.FirstOrDefault().userFriendID = u.userFriendID;
                query.FirstOrDefault().userID = u.userID;

                context.SaveChanges();
            }
        }

    }
}