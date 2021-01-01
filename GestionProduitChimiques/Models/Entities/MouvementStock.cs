using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionProduitChimiques.Models.Entities
{
    public class MouvementStock
    {
        public int Id { get; set; }
        public DateTime? DateMouvement { get; set; }
        public string TypeMvt { get; set; }
        public string Raison { get; set; }
        public int IdProduit { get; set; }
        public int Quantite { get; set; }
        public string Observation { get; set; }
        public string UniteMesure { get; set; }
        public Produit produit { get; set; }
    }
}
