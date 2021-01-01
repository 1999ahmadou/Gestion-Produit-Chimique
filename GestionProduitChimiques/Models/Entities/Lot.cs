using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionProduitChimiques.Models.Entities
{
    public class Lot
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int IdProduit { get; set; }
        public string Purete { get; set; }
        public string Concentration { get; set; }
        public DateTime? DatePeremption { get; set; }
        public String UniteMesure {get;set;}
        public int Stock { get; set; }
    }
}
