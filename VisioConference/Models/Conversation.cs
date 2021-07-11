using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VisioConference.Models
{
    public class Conversation
    {
        [Key]
        public int convID { get; set; }
        [Required]
        public int userID { get; set; }
        [Required]
        public int userFriendID { get; set; }
        [StringLength(10000)]
        public string message { get; set; }
        public virtual User user { get; set; }

        public Conversation()
        {
        }

        public Conversation(int convID, int userID, int userFriendID, string message, User user)
        {
            this.convID = convID;
            this.userID = userID;
            this.userFriendID = userFriendID;
            this.message = message;
            this.user = user;
        }
    }
}