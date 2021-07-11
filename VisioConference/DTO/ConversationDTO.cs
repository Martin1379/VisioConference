using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VisioConference.Models;
using VisioConference.Repository.Interface;

namespace VisioConference.DTO
{
    public class ConversationDTO 
    {
        public int convID { get; set; }
        public int userID { get; set; }
        public int userFriendID { get; set; }
        public string message { get; set; }
        public virtual User user { get; set; }

        public ConversationDTO()
        {
        }

        public ConversationDTO(int convID, int userID, int userFriendID, string message, User user)
        {
            this.convID = convID;
            this.userID = userID;
            this.userFriendID = userFriendID;
            this.message = message;
            this.user = user;
        }
    }
}