using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VisioConference.Repository.Objets
{
    public class Conversation
    {
        [Key]
        public int convID { get; set; }
        [StringLength(255)]
        [Required]
        public string userID { get; set; }
        [StringLength(255)]
        [Required]
        public string userFriendID { get; set; }
        [StringLength(10000)]
        public string message { get; set; }
        public virtual User user { get; set; }
    }
}