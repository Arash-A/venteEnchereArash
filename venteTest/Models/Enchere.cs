using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace venteTest.Models
{
    public class Enchere
    {

        public virtual int EnchereId { get; set; }

        public virtual double Niveau { get; set; }

        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public  int ObjetId { get; set; }

        public  Objet Objet { get; set; }

        public  Evaluation Evaluation { get; set; }

    }
}