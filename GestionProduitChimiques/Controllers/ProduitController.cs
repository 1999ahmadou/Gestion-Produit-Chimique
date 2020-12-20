using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GestionProduitChimiques.Models;
using GestionProduitChimiques.Models.BLL;
using GestionProduitChimiques.Models.Entities;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace GestionProduitChimiques.Controllers
{
    public class ProduitController : Controller
    {
        IWebHostEnvironment hostingEnvironment;
        public ProduitController(IWebHostEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment; // Microsoft Dependency injection by Controller
        }
        public IActionResult Index()
        {
            List<Produit> produits = BLL_Produit.GetAll();
            List<Lot> meslots = BLL_Lot.GetAll();
            ViewBag.MesLots = meslots;
            ViewBag.ListProduit = produits;
            return View();
        }
        [HttpPost]
        public JsonResult AddProduit(Produit produit)
        {
            try
            {
                /*string uniqueFileName = null;
                var uploadfiles = Request.Form.Files;
                if (uploadfiles != null)
                {
                    foreach (var upfile in uploadfiles)
                    {

                        string uploadfile = Path.Combine(hostingEnvironment.WebRootPath, "FSD");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + upfile.FileName;
                        string Filepath = Path.Combine(uploadfile, uniqueFileName);
                        upfile.CopyTo(new FileStream(Filepath, FileMode.Create));
                    }
                }*/
                

                BLL_Produit.Add(produit);
                return Json(new { success = true, message = "Ajout Effectuez" });
            }
            catch (Exception Ex)
            {
                return Json(new { success = false, message = Ex.Message });
            }
        }
        [HttpPost]
        public JsonResult UpdateProduit(int Id, Produit produit)
        {
            try
            {
                BLL_Produit.Update(Id, produit);
                return Json(new { success = true, message = "Modification Effectuez" });
            }
            catch (Exception Ex)
            {
                return Json(new { success = false, message = Ex.Message });
            }
        }
        [HttpPost]
        public JsonResult DeleteProduit(int Id)
        {
            try
            {
                Produit produit = BLL_Produit.GetProduit(Id);
                if (produit.Perissable == 1)
                {
                    BLL_Produit.DeleteLot(Id);
                    BLL_Produit.DeleteMouvement(Id);
                    BLL_Produit.Delete(Id);
                }
                else
                {
                    BLL_Produit.DeleteMouvement(Id);
                    BLL_Produit.Delete(Id);
                }
                return Json(new { success = true, message = "Suppression Effectuez" });
            }
            catch (Exception Ex)
            {
                return Json(new { success = false, message = Ex.Message });
            }
        }

        [HttpGet]
        public Produit GetProduit(int Id)
        {
            return BLL_Produit.GetProduit(Id);
        }
        [HttpGet]
        public JsonResult GetProduitAndLot(int Id)
        {
            try
            {
                Produit produit= BLL_Produit.GetProduit(Id);
                return Json(new { success = true, message = produit });
            }
            catch(Exception Ex)
            {
                return Json(new { success = false, message = Ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetEtatPhysique(int Id)
        {
            try
            {
                Produit produit = BLL_MouvementStock.GetProduit(Id);
                return Json(new { success = true, type = produit.EtatPhysique });
            }
            catch (Exception Ex)
            {
                return Json(new { success = false, message = Ex.Message });
            }
        }
    }
}
