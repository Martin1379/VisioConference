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
        public int id { get; set; }
        [StringLength(255)]
        [Index(IsUnique = true)]
        [Required]
        public string login { get; set; }

        //Indique dans la BD si l'utilisateur est en connexion ou pas - lorsqu'il ferme l'app, il met la valeur à false
        public bool connected { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Conversation> conversations { get; set; }

        public User(int id, string login, bool connected)
        {
            this.id = id;
            this.login = login;
            this.connected = connected;
        }
    }
}