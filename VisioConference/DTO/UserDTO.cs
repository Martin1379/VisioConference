using System;
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
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Votre adresse eMail n'est pas valide.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Veuillez indiquer votre mot de passe.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ResetPassewordCode { get; set; }
        public string Photo { get; set; }
        public int Etat { get; set; }
        public bool IsAdmin { get; set; }
        public virtual ICollection<Conversation> conversations { get; set; }

        public UserDTO(int id, string pseudo)
        {
            Id = id;
            Pseudo = pseudo;
        }
        //Constructeur pour la méthode de recherche d'ami, on ne retourne pas le mdp
        public UserDTO(string email, string pseudo, string photo, int etat, int id)
        {
            Id = id;
            Email = email;
            Password = "";
            ResetPassewordCode = "";
            Pseudo = pseudo;
            Photo = photo;
            Etat = etat;
        }

        public UserDTO(string email, string password, string resetPassewordCode, string pseudo, string photo, int etat, int id)
        {
            Id = id;
            Email = email;
            Password = password;
            ResetPassewordCode = resetPassewordCode;
            Pseudo = pseudo;
            Photo = photo;
            Etat = etat;
        }
    }
}