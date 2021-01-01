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

        public IActionResult MvtEntrant()
        {
            ViewBag.ListProduits = BLL_Produit.GetAll();
            ViewBag.ListMouvement = BLL_MouvementStock.GetAllEntrant("Entrant");
            return View("MouvementEntrant");
        }

        public IActionResult MvtSortant()
        {
            ViewBag.ListProduits = BLL_Produit.GetAll();
            ViewBag.ListMouvement = BLL_MouvementStock.GetAllSortant("Sortant");
            return View("MouvementSortant");
        }

        [HttpPost]
        public JsonResult AddMouvement(MouvementStock mouvement,Lot lot)
        {
            try
            {
                Boolean reponse = BLL_MouvementStock.Add(mouvement, lot);
                if (reponse)
                {
                    return Json(new { success = true, message = "Operation effectuez" });
                }
                else
                {
                    return Json(new { success = false, message = "Operation non effectuez" });
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
                MouvementStock mouvement = BLL_MouvementStock.GetMouvement(Id);
                Produit produit = BLL_Produit.GetProduit(mouvement.IdProduit);
                if (produit.Perissable == 0)
                {
                    BLL_MouvementStock.UpdateStockProduit(produit.Id, produit.Stock - mouvement.Quantite);
                    BLL_MouvementStock.Delete(Id);
                }
                else
                {
                    BLL_MouvementStock.Delete(Id);
                }
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
                return Json(new { success = true,type = produit.Perissable, etatphy=produit.EtatPhysique,unitemesure=produit.UniteMesure });
            }
            catch (Exception Ex)
            {
                return Json(new { success = false, message = Ex.Message });
            }
        }
    }
}
