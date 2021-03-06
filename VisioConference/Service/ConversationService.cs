using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VisioConference.DTO;
using VisioConference.Repository.DAO;

namespace VisioConference.Service
{

    public class ConversationService
    {
        private ConversationDAO repo = new ConversationDAO();

        public List<ConversationDTO> findAll()
        {
            return repo.findAll();
        }
        public ConversationDTO findByUsers(UserDTO user1, UserDTO user2)
        {
            ConversationDTO conv = new ConversationDTO();
            conv = repo.findByUsers(user1, user2);
            return conv;
        }
        public List<UserDTO> findFriendAdmin(UserDTO user)
        {
            return repo.findFriendAdmin(user);
        }
        public List<JointureDTO> findFriends(UserDTO user)
        {
            return repo.findFriends(user);
        } 
        public List<JointureDTO> findFriends(UserDTO user, string search)
        {
            return repo.findFriends(user, search);
        }
        public List<JointureDTO> findFriendsAndOthers(UserDTO user, string search)
        {
            return repo.findFriendsAndOthers(user, search);
        }
        public void AddConversation(int user, int receiver)
        {
            repo.AddConversation(user, receiver);
        }
        public void removeConversation(int convId)
        {
            repo.removeConversation(convId);
        }
        public void modifyMessage(ConversationDTO u, string message)
        {
            repo.modifyMessage(u, message);
        }
        public void Update(ConversationDTO u)
        {
            repo.Update(u);
        }
    }
}