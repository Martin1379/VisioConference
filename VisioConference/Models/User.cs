using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VisioConference.Models
{
    public class User
    {
        public User()
        {
            //Assure que l'objet ICollection<Conversation> est bien initialisé
            conversations = new HashSet<Conversation>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        [Required]
        public string Pseudo { get; set; }
        [Required]
        [StringLength(255)]
        [Index(IsUnique = true)]
        public string Email { get; set; }
        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [FileExtensions(Extensions = "png, jpg, jpeg")]
        public string Photo { get; set; } 

        public int Etat { get; set; }

        public bool IsAdmin { get; set; }

        //[Required]
        [StringLength(255)]
        public string ResetPassewordCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Conversation> conversations { get; set; }

    }
}