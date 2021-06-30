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
        List<UserDTO> findFriends(UserDTO user);
        void modifyMessage(ConversationDTO u, string message);
        void Update(ConversationDTO u);
    }
}
