using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace GestionProduitChimiques.Models.Entities
{
    public class Produit
    {
        public int Id { get; set; }

        [Required]
        public string Reference { get; set; }

        [Required]
        public string Nom { get; set; }
        [Required]
        public string Formule { get; set; }
        [Required]
        public string CAS { get; set; }
        public string Toxicite { get; set; }
        public string EtatPhysique { get; set; }
        public string UniteMesure { get; set; }
        public Byte Perissable { get; set; }
        public int TempMinStockage { get; set; }
        public int TempMaxStockage { get; set; }
        public string ConditionStockage { get; set; }
        public string TypeGestion { get; set; }
        public int StockMin { get; set; }
        public int Stock { get; set; }
        public List<Lot> lots { get; set; }
    }
}
