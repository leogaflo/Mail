using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MailServices.Models
{
    public class User
    {
        [Key]
        [Column("User",Order = 0)]
        public int UserID { get; set; }
        [Key]
        [Column("Mail",Order =1)]
        public int MailUserID { get; set; }

        [Required(ErrorMessage = "Este Campo es Requerido")]
        [StringLength(25, ErrorMessage = "Porfavor introduzca un nombre con entre 3 y 25 caracteres", MinimumLength = 3)]
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Este Campo es Requerido")]
        [StringLength(25, ErrorMessage = "Porfavor introduzca un apellido con entre 3 y 25 caracteres", MinimumLength = 3)]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Este Campo es Requerido")]
        [DataType(DataType.Date)]
        [Display(Name = "Cumpleaños")]
        public String Birthday { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Este Campo es Requerido")]
        [Display(Name = "Correo")]
        public string Email { get; set; }
        /*That is the provider email password this password */
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Este Campo es Requerido")]
        [Display(Name = "Contraseña Correo")]
        public string EmailPassword { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Este Campo es Requerido")]
        [Display(Name = "Contraseña de Usuario")]
        /*That is you own password use to sing in on this app*/
        public string UserPassword { get; set; }
        [Display(Name = "Proveedor")]
        public string Provider { get; set; }
        [Display(Name = "Cuenta Administador")]
        public bool Admin { get; set; }
        public ICollection<Contacto> UserContacts { get; set; }
    }
}