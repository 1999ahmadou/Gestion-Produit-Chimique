using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionProduitChimiques.Models.BLL;
using GestionProduitChimiques.Models.DAL;
using GestionProduitChimiques.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionProduitChimiques.Controllers
{
    public class MouvementController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ListProduits = BLL_Produit.GetAll();
            ViewBag.ListMouvement = BLL_MouvementStock.GetAll();
            return View();
        }

        [HttpPost]
        public JsonResult AddMouvement(MouvementStock mouvement,Lot lot)
        {
            try
            {
                Produit produit = BLL_MouvementStock.GetProduit(mouvement.IdProduit);
                if (produit.Perissable == 1)//Gestion par Lot
                {
                    if(mouvement.TypeMvt== "Entrant")
                    {
                        Lot l = DAL_MouvementStock.VerifIfLotExiste(lot.IdProduit);
                        if (l.IdProduit == lot.IdProduit && l.Concentration == lot.Concentration && l.Purete == lot.Purete && l.DatePeremption == lot.DatePeremption)
                        {
                            BLL_MouvementStock.Add(mouvement);
                            BLL_MouvementStock.UpdateStockLot(l.Id, mouvement.Quantite + l.Stock);
                            BLL_MouvementStock.UpdateStockProduit(mouvement.IdProduit, mouvement.Quantite + produit.Stock);
                            return Json(new { success = true, message = "Ajout Effectuez" });
                        }
                        else
                        {
                            BLL_MouvementStock.Add(mouvement);
                            BLL_Lot.Add(lot);
                            BLL_MouvementStock.UpdateStockProduit(mouvement.IdProduit, mouvement.Quantite + produit.Stock);
                            return Json(new { success = true, message = "Ajout Effectuez" });
                        }
                    }
                    else
                    {
                        Lot l = DAL_MouvementStock.VerifIfLotExiste(lot.IdProduit);
                        if(l.IdProduit==lot.IdProduit && l.Concentration==lot.Concentration && l.Purete==lot.Purete && l.DatePeremption == lot.DatePeremption)
                        {
                            BLL_MouvementStock.Add(mouvement);
                            BLL_MouvementStock.UpdateStockProduit(mouvement.IdProduit, produit.Stock - mouvement.Quantite);
                            BLL_MouvementStock.UpdateStockLot(l.Id, l.Stock - lot.Stock);
                            return Json(new { success = true, message = "Ajout Effectuez" });
                        }
                        else
                        {
                            return Json(new { success = false, message = "Aucun lot ne correspond aux données saisies." });
                        }
                    }
                }
                else//Gestion Global
                {
                    if(mouvement.TypeMvt== "Entrant")
                    {
                        BLL_MouvementStock.Add(mouvement);
                        BLL_MouvementStock.UpdateStockProduit(mouvement.IdProduit, mouvement.Quantite + produit.Stock);
                        return Json(new { success = true, message = "Ajout Effectuez" });
                    }
                    else
                    {
                        BLL_MouvementStock.Add(mouvement);
                        BLL_MouvementStock.UpdateStockProduit(mouvement.IdProduit,produit.Stock- mouvement.Quantite);
                        return Json(new { success = true, message = "Ajout Effectuez" });
                    }
                }
                
            }
            catch (Exception Ex)
            {
                return Json(new { success = false, message = Ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteMouvement(int Id)
        {
            try
            {
                BLL_MouvementStock.Delete(Id);
                return Json(new { success = true, message = "Suppression Effectuez" });
            }
            catch (Exception Ex)
            {
                return Json(new { success = false, message = Ex.Message });
            }
        }

        [HttpGet]
        public MouvementStock GetMouvement(int Id)
        {
            return BLL_MouvementStock.GetMouvement(Id);
        }

        [HttpPost]
        public JsonResult UpdateMouvement(int Id, MouvementStock mouv)
        {
            try
            {
                BLL_MouvementStock.Update(Id, mouv);
                return Json(new { success = true, message = "Modification Effectuez" });
            }
            catch (Exception Ex)
            {
                return Json(new { success = false, message = Ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetProduit(int Id)
        {
            try
            {
                Produit produit= BLL_MouvementStock.GetProduit(Id);
                return Json(new { success = true,type = produit.Perissable });
            }
            catch (Exception Ex)
            {
                return Json(new { success = false, message = Ex.Message });
            }
        }
    }
}
