using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VisioConference.Repository.Objets
{
    public class User
    {
        public User()
        {
            conversations = new HashSet<Conversation>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        [Index(IsUnique = true)]
        [Required]
        public string Login { get; set; }

        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        [FileExtensions(Extensions = "png, jpg, jpeg")]
        public string Photo { get; set; }

        public bool Connected { get; set; }

        public bool IsAdmin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Conversation> conversations { get; set; }

        public User(int id, string login)
        {
            Id = id;
            Login = login;
        }
        //Constructeur pour la méthode de revherche d'ami
        public User(string email, string login, string photo, bool connected)
        {
            Email = email;
            Password = "";
            Login = login;
            Photo = photo;
            Connected = false;
        }


        public User(string email, string password, string login, string photo, bool connected, int id)
        {
            Id = id;
            Email = email;
            Password = password;
            Login = login;
            Photo = photo;
            Connected = connected;
        }
    }
}