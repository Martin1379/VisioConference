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
        ConversationDTO findByUsers(User user1, User user2);
        List<User> findFriends(User user);
        void modifyMessage(Conversation u, string message);
        void Update(Conversation u);
    }
}
