using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisioConference.DTO
{
    public class JointureDTO
    {
        public int Id { get; set; }
        public string Pseudo { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public int Etat { get; set; }
        public bool IsAdmin { get; set; }
        public int convID { get; set; }
        public int userID { get; set; }
        public int userFriendID { get; set; }
        public string message { get; set; }
        public bool invitation { get; set; }
        public JointureDTO()
        {
        }

        public JointureDTO(int id, string pseudo, string email, string photo, int etat, int userID, int userFriendID, bool invitation)
        {
            Id = id;
            Pseudo = pseudo;
            Email = email;
            Photo = photo;
            Etat = etat;
            this.userID = userID;
            this.userFriendID = userFriendID;
            this.invitation = invitation;
        }
    }
}