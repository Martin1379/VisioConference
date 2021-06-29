using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisioConference.DTO
{
    public class ConversationDTO
    {
        public int convID { get; set; }
        public int userID { get; set; }
        public int userFriendID { get; set; }
        public string message { get; set; }
        //public virtual User user { get; set; }
    }
}