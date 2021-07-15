﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using VisioConference.Models;

namespace VisioConference.DTO
{
    public class UserDTO
    {
        public UserDTO()
        {
            conversations = new HashSet<Conversation>();
        }

        public int Id { get; set; }
        public string Pseudo { get; set; }

        [Required(ErrorMessage = "Veuillez indiquer votre adresse eMail.")]
        [EmailAddress(ErrorMessage = "Adresse eMail incorrecte.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Veuillez indiquer votre mot de passe.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Photo { get; set; }
        public bool Connected { get; set; }
        public bool IsAdmin { get; set; }
        public virtual ICollection<Conversation> conversations { get; set; }

        public UserDTO(int id, string pseudo)
        {
            Id = id;
            Pseudo = pseudo;
        }
        //Constructeur pour la méthode de recherche d'ami, on ne retourne pas le mdp
        public UserDTO(string email, string pseudo, string photo, bool connected, int id)
        {
            Id = id;
            Email = email;
            Password = "";
            Pseudo = pseudo;
            Photo = photo;
            Connected = connected;
        }


        public UserDTO(string email, string password, string pseudo, string photo, bool connected, int id)
        {
            Id = id;
            Email = email;
            Password = password;
            Pseudo = pseudo;
            Photo = photo;
            Connected = connected;
        }
    }
}