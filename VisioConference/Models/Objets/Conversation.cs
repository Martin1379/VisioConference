using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VisioConference.Models.Objets
{
    public class Conversation
    {
        [Key]
        public int convID { get; set; }
        [Required]
        public int userID { get; set; }
        
        
        //[StringLength(255)]
        [Required]
        public int userFriendID { get; set; }
        [StringLength(10000)]
        public string message { get; set; }
        public virtual User user { get; set; }

    }
}