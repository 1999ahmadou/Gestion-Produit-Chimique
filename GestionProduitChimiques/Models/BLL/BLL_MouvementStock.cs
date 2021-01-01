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
        public static Boolean Add(MouvementStock mouvement, Lot lot)
        {
            Boolean ReponseRequette=false;
            Produit produit = BLL_Produit.GetProduit(mouvement.IdProduit);
            int IdentifiantLot = DAL_Lot.ShowIfLotExistAndReturnId(lot);

            if (produit.Perissable == 1) //Gestion du produit se fait par lot
            {
                if (mouvement.TypeMvt == "Entrant")
                {
                    if (IdentifiantLot >= 1)
                    {
                        Lot lot1 = BLL_Lot.GetLot(IdentifiantLot);
                        DAL_MouvementStock.Add(mouvement);
                        DAL_MouvementStock.UpdateStockLot(IdentifiantLot, lot.Stock + lot1.Stock);
                        DAL_MouvementStock.UpdateStockProduit(mouvement.IdProduit, produit.Stock + mouvement.Quantite);
                        ReponseRequette= true;                        
                    }
                    else
                    {
                        DAL_MouvementStock.Add(mouvement);
                        BLL_Lot.Add(lot);
                        DAL_MouvementStock.UpdateStockProduit(mouvement.IdProduit, produit.Stock + mouvement.Quantite);
                        ReponseRequette= true;
                    }
                }
                else
                {
                    if (IdentifiantLot>=1)
                    {
                        Lot lot1 = BLL_Lot.GetLot(IdentifiantLot);
                        if (lot.Stock > produit.Stock)
                        {
                            ReponseRequette= false;
                        }
                        else if (lot.Stock == lot1.Stock)
                        {
                            DAL_MouvementStock.Add(mouvement);
                            DAL_MouvementStock.UpdateStockProduit(mouvement.IdProduit, produit.Stock - lot.Stock);
                            BLL_Lot.Delete(IdentifiantLot);
                            return true;
                        }
                        else
                        {
                            
                            if (lot1.Stock < lot.Stock)
                            {
                                ReponseRequette= false;
                            }
                            else
                            {
                                DAL_MouvementStock.Add(mouvement);
                                DAL_MouvementStock.UpdateStockProduit(mouvement.IdProduit, produit.Stock - mouvement.Quantite);
                                DAL_MouvementStock.UpdateStockLot(IdentifiantLot, lot1.Stock - lot.Stock);
                                ReponseRequette= true;
                            }
                        }
                    }
                    else
                    {
                        ReponseRequette= false;
                    }
                }
            }
            else  // Gestion du produit se fait globalement
            {
                if (mouvement.TypeMvt == "Entrant")
                {
                    DAL_MouvementStock.Add(mouvement);
                    DAL_MouvementStock.UpdateStockProduit(mouvement.IdProduit, mouvement.Quantite + produit.Stock);
                    ReponseRequette= true;
                }
                else
                {
                    if (mouvement.Quantite > produit.Stock)
                    {
                        ReponseRequette= false;
                    }
                    else
                    {
                        DAL_MouvementStock.Add(mouvement);
                        DAL_MouvementStock.UpdateStockProduit(mouvement.IdProduit,produit.Stock-mouvement.Quantite);
                        ReponseRequette= true;
                    }
                }
            }
            return ReponseRequette;
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

        public static List<MouvementStock> GetAllEntrant(string typemvt)
        {
            return DAL_MouvementStock.SelectAllEntrant(typemvt);
        }

        public static List<MouvementStock> GetAllSortant(string typemvt)
        {
            return DAL_MouvementStock.SelectAllSortant(typemvt);
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
