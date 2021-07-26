using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisioConference.DTO;
using VisioConference.Models;

namespace VisioConference.Repository.Interface
{
    public interface IConversation
    {
        List<ConversationDTO> findAll();
        ConversationDTO findByUsers(UserDTO user1, UserDTO user2);
        List<UserDTO> findFriendAdmin(UserDTO user);
        List<JointureDTO> findFriends(UserDTO user);
        void AddConversation(int user, int receiver);
        void modifyMessage(ConversationDTO u, string message);
        void removeConversation(int convId);
        void Update(ConversationDTO u);
    }
}
