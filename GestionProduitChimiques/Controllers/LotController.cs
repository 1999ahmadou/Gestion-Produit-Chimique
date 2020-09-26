using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionProduitChimiques.Models.BLL;
using GestionProduitChimiques.Models.DAL;
using GestionProduitChimiques.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GestionProduitChimiques.Controllers
{
    public class LotController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ListLot = BLL_Lot.GetAll();
            return View();
        }
       /* [HttpPost]
        public JsonResult AddLot(Lot lot)
        {
            try
            {
                if (DAL_Lot.VerifIfProduitExiste(lot.IdProduit)==1)
                {
                    BLL_Lot.Add(lot);
                    return Json(new { success = true, message = "Ajout Effectuez" });
                }
                else
                {
                    return Json(new { success = false, message = "Cet Produit n'existe pas, Veillez d'abord l'ajoutez" });
                }
            }
            catch(Exception Ex)
            {
                return Json(new { success = false, message = Ex.Message });
            }
        }*/

        [HttpGet]
        public Lot GetLot(int Id)
        {
            return BLL_Lot.GetLot(Id);
        }

        [HttpPost]
        public JsonResult UpdateLot(int Id, Lot lot)
        {
            try
            {
                BLL_Lot.Update(Id, lot);
                return Json(new { success = true, message = "Modification Effectuez" });
            }
            catch (Exception Ex)
            {
                return Json(new { success = false, message = Ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteLot(int Id)
        {
            try
            {
                BLL_Lot.Delete(Id);
                return Json(new { success = true, message = "Suppression Effectuez" });
            }
            catch (Exception Ex)
            {
                return Json(new { success = false, message = Ex.Message });
            }
        }
        public Produit Details(int id)
        {
            return BLL_Lot.SelectProduit(id); 
        }
    }
}
