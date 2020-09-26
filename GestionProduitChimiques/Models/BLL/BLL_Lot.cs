using GestionProduitChimiques.Models.DAL;
using GestionProduitChimiques.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionProduitChimiques.Models.BLL
{
    public class BLL_Lot
    {
        public static void Add(Lot lot)
        {
            DAL_Lot.Add(lot);
        }

        public static void Update(int id, Lot lot)
        {
            DAL_Lot.Update(id, lot);
        }

        public static void Delete(int pId)
        {
            DAL_Lot.Delete(pId);
        }
        public static Lot GetLot(int id)
        {
            return DAL_Lot.SelectById(id);
        }

        public static Produit SelectProduit(int id)
        {
            return DAL_Lot.SelectProduit(id);
        }

        public static List<Lot> GetAll()
        {
            return DAL_Lot.SelectAll();
        }
    }
}
