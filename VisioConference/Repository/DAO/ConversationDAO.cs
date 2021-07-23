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
        public List<ConversationDTO> findAll()
        {
            using (MyContext context = new MyContext())
            {
                List<ConversationDTO> lst = new List<ConversationDTO>();
                ConversationDTO dto = new ConversationDTO();

                var query = context.conversations; // <=> SELECT * FROM conversation
                foreach (Conversation item in query)
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
                dto = Convertisseur.ConvDTOFromConv(dto, query.FirstOrDefault());
                return dto;
            }
        }
        public List<UserDTO> findFriendAdmin(UserDTO user)
        {
            using (MyContext context = new MyContext())
            {
                var query = (from us in context.users
                             join co in context.conversations on us.Id equals co.userID
                             where co.userFriendID == user.Id
                             select new UserDTO(us.Email, us.Password, us.ResetPassewordCode, us.Pseudo, us.Photo, us.Etat, us.Id)
                            )
                            .Union
                            (from us in context.users
                             join co in context.conversations on us.Id equals co.userFriendID
                             where co.userID == user.Id
                             select new UserDTO(us.Email, us.Password, us.ResetPassewordCode, us.Pseudo, us.Photo, us.Etat, us.Id)
                             )
                            ;

                List<UserDTO> lst = new List<UserDTO>();
                foreach (UserDTO item in query)
                {
                    lst.Add(new UserDTO()
                    {
                        Email = item.Email,
                        Pseudo = item.Pseudo,
                        Password = item.Password,
                        Photo = item.Photo,
                        Etat = item.Etat,
                        Id = item.Id,
                    });
                }
                return lst;
            }
        }
        public List<JointureDTO> findFriends(UserDTO user)
        {
            using (MyContext context = new MyContext())
            {
                var query = (from us in context.users
                             join co in context.conversations on us.Id equals co.userID
                             where co.userFriendID == user.Id
                             select new JointureDTO(us.Id, us.Pseudo, us.Email, us.Photo, us.Etat, co.userID, co.userFriendID, co.invitation)
                             )
                            .Union
                            (from us in context.users
                             join co in context.conversations on us.Id equals co.userFriendID
                             where co.userID == user.Id
                             select new JointureDTO(us.Id, us.Pseudo, us.Email, us.Photo, us.Etat, co.userID, co.userFriendID, co.invitation)
                             )
                            ;

                List<JointureDTO> lst = new List<JointureDTO>();
                foreach (var item in query)
                {
                    lst.Add(new JointureDTO() {
                        Email = item.Email,
                        Pseudo = item.Pseudo,
                        Photo = item.Photo,
                        Etat = item.Etat,
                        Id = item.Id,
                        invitation = item.invitation,
                        userID = item.userID,
                        userFriendID = item.userFriendID
                    });
                }
                return lst;
            }
        }
        public List<JointureDTO> findFriends(UserDTO user, string search)
        {
            using (MyContext context = new MyContext())
            {
                var query = (
                             from us in context.users
                             join co in context.conversations on us.Id equals co.userID
                             where co.userFriendID == user.Id && us.Pseudo.Contains(search)
                             select new JointureDTO(us.Id, us.Pseudo, us.Email, us.Photo, us.Etat, co.userID, co.userFriendID, co.invitation )
                             )
                            .Union
                            (
                             from us in context.users
                             join co in context.conversations on us.Id equals co.userFriendID
                             where co.userID == user.Id && us.Pseudo.Contains(search)
                             select new JointureDTO(us.Id, us.Pseudo, us.Email, us.Photo, us.Etat, co.userID, co.userFriendID, co.invitation)
                             )
                            ;

                List<JointureDTO> lst = new List<JointureDTO>();
                foreach (JointureDTO item in query)
                {
                    lst.Add(new JointureDTO()
                    {
                        Email = item.Email,
                        Pseudo = item.Pseudo,
                        Photo = item.Photo,
                        Etat = item.Etat,
                        Id = item.Id,
                        invitation = item.invitation,
                        userID = item.userID,
                        userFriendID = item.userFriendID
                    });
                }
                return lst;
            }
        }
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
                query.invitation = u.invitation;

                context.SaveChanges();
            }
        }
    }
}