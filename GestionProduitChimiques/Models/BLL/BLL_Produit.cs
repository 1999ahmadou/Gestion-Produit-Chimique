using GestionProduitChimiques.Models.DAL;
using GestionProduitChimiques.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionProduitChimiques.Models.BLL
{
    public class BLL_Produit
    {
        public static void Add(Produit produit)
        {
            DAL_Produit.Add(produit);
        }

        public static void Update(int id, Produit produit)
        {
            DAL_Produit.Update(id, produit);
        }

        public static void Delete(int pId)
        {
            DAL_Produit.Delete(pId);
        }
        public static void DeleteMouvement(int pId)
        {
            DAL_Produit.DeleteMouvement(pId);
        }

        public static void DeleteLot(int pId)
        {
            DAL_Produit.DeleteLot(pId);
        }
        public static Produit GetProduit(int id)
        {
            return DAL_Produit.SelectById(id);
        }
        public static List<Produit> GetAll()
        {
            return DAL_Produit.SelectAll();
        }

        public static List<Lot> GetAllLotOfProduit(int id)
        {
            return DAL_Produit.GetAllLotOfProduit(id);
        }
    }
}
