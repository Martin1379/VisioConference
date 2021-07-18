using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VisioConference.Repository.Interface;
using VisioConference.Models;
using VisioConference.DTO;
using VisioConference.Tools;

namespace VisioConference.Repository.DAO
{
    public class ConversationDAO : IConversation
    {
        // Accès en ligne, il faut que les méthodes soient async et utiliser HasNotracking pour les histoire de cache
        // => OU utiliser le using Mycontext dans les méthodes
        public List<ConversationDTO> findAll()
        {
            using (MyContext context = new MyContext())
            {
                List<ConversationDTO> lst = new List<ConversationDTO>();
                ConversationDTO dto = new ConversationDTO();

                var query = context.conversations; // <=> SELECT * FROM conversation
                foreach (var item in query)
                {
                    dto = Convertisseur.ConvDTOFromConv(dto, item);
                    lst.Add(dto);
                }
                return lst;
            }
        }


        public ConversationDTO findByUsers(UserDTO user1, UserDTO user2)
        {
            using (MyContext context = new MyContext())
            {
                var query = from co in context.conversations
                            where (co.userFriendID == user1.Id && co.userID == user2.Id) || (co.userFriendID == user2.Id && co.userID == user1.Id)
                            select co;
                ConversationDTO dto = new ConversationDTO();
                dto = Convertisseur.ConvDTOFromConv(dto, query.FirstOrDefault()); //TODO try catch
                return dto;
            }
        }

        public List<UserDTO> findFriends(UserDTO user)
        {
            using (MyContext context = new MyContext())
            {
                var query = (from us in context.users
                             join co in context.conversations on us.Id equals co.userID
                             where co.userFriendID == user.Id
                             select new
                             {
                                 FriendId = us.Id,
                                 FriendMail = us.Email,
                                 FriendPseudo = us.Pseudo,
                                 FriendPhoto = us.Photo,
                                 FriendConnected = us.Etat
                             })
                            .Union
                            (from us in context.users
                             join co in context.conversations on us.Id equals co.userFriendID
                             where co.userID == user.Id
                             select new
                             {
                                 FriendId = us.Id,
                                 FriendMail = us.Email,
                                 FriendPseudo = us.Pseudo,
                                 FriendPhoto = us.Photo,
                                 FriendConnected = us.Etat
                             })
                            ;

                List<UserDTO> lst = new List<UserDTO>();
                foreach (var item in query)
                {
                    lst.Add(new UserDTO(item.FriendMail,item.FriendPseudo, item.FriendPhoto, item.FriendConnected, item.FriendId));
                }
                return lst;
            }
        }

        /// <summary>
        /// Modifie la conversation (suite à envoi de message), si dépasse une certaine taille, on supprime les messages les plus anciens jusqu'à avoir la taille désirée
        /// </summary>
        /// <param name="u"></param>
        /// <param name="message"></param>
        public void modifyMessage(ConversationDTO u, string message)
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

        public void removeConversation(int convId)
        {
            using (MyContext context = new MyContext())
            {
                var query = (from co in context.conversations
                            where convId == co.convID
                            select co).FirstOrDefault();

                if (query != null)
                {
                    context.conversations.Remove(query);
                }

                context.SaveChanges();
            }
        }

        public void Update(ConversationDTO u)
        {
            using (MyContext context = new MyContext())
            {
                var query = (from co in context.conversations
                            where u.convID == co.convID
                            select co).FirstOrDefault();
                query.message = u.message;
                query.userFriendID = u.userFriendID;
                query.userID = u.userID;

                context.SaveChanges();
            }
        }

    }
}