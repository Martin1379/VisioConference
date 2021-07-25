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
        public int convId { get; set; }
        public int userId { get; set; }
        public int userFriendId { get; set; }
        public string message { get; set; }
        public bool invitation { get; set; }
        public JointureDTO()
        {
        }

        public JointureDTO(int id, string pseudo, string email, string photo, int etat, int userId, int userFriendId, bool invitation)
        {
            Id = id;
            Pseudo = pseudo;
            Email = email;
            Photo = photo;
            Etat = etat;
            this.userId = userId;
            this.userFriendId = userFriendId;
            this.invitation = invitation;
        }

        public JointureDTO(int id, string pseudo, string email, string photo, int etat)
        {
            Id = id;
            Pseudo = pseudo;
            Email = email;
            Photo = photo;
            Etat = etat;
            userId = 0;
            userFriendId = 0;
            invitation = false;
        }
    }
}