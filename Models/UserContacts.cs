﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MailServices.Models
{
    public class UserContacts
    {

        [Key]
        public int IdContacts { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        public string FechaNac { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Sexo { get; set; }

        //Foreign key
        public int UserID { get; set; }

        public User User { get; set; }
    }
}