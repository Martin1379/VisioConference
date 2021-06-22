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
        MyContext context = new MyContext();
        public List<Conversation> findAll()
        {
            List<Conversation> lst = new List<Conversation>();

            //List<Utilisateur> utilisateurs = context.utilisateurs.Include(u => u.login).ToList();
            // <=> SELECT * FROM utilisateur
            var query = context.conversations;
            foreach (var item in query)
            {
                lst.Add(item);
            }
            return lst;
        }

        public Conversation findByUsers(string user1, string user2)
        {
            throw new NotImplementedException();
        }

        public List<User> findFriends(string user)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Modifie la conversation (suite à envoi de message), si dépasse une certaine taille, on supprime les messages les plus anciens jusqu'à avoir la taille désirée
        /// </summary>
        /// <param name="u"></param>
        /// <param name="message"></param>
        public void modifyMessage(Conversation u, string message)
        {
            throw new NotImplementedException();
        }

        public void Update(Conversation u)
        {
            throw new NotImplementedException();
        }
    }
}