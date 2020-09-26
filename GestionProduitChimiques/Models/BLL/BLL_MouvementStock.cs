using GestionProduitChimiques.Models.DAL;
using GestionProduitChimiques.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionProduitChimiques.Models.BLL
{
    public class BLL_MouvementStock
    {
        public static void Add(MouvementStock mouvement)
        {
            DAL_MouvementStock.Add(mouvement);
        }

        public static void Update(int id, MouvementStock mouvement)
        {
            DAL_MouvementStock.Update(id, mouvement);
        }

        public static void Delete(int pId)
        {
            DAL_MouvementStock.Delete(pId);
        }
        public static MouvementStock GetMouvement(int id)
        {
            return DAL_MouvementStock.SelectById(id);
        }

        public static Produit GetProduit(int id)
        {
            return DAL_Produit.SelectById(id);
        }

        public static List<MouvementStock> GetAll()
        {
            return DAL_MouvementStock.SelectAll();
        }

        public static void UpdateStockProduit(int EntityKey,int Stock)
        {
            DAL_MouvementStock.UpdateStockProduit(EntityKey,Stock);
        }
        public static void UpdateStockLot(int EntityKey,int Stock)
        {
            DAL_MouvementStock.UpdateStockLot(EntityKey,Stock);
        }
    }
}
