using E_Cart.Models;
using E_Cart.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Cart.Controllers
{
    public class ItemController : Controller
    {
        private ECartDB3Entities objECartDB3Entities;
        public ItemController()
        {
            objECartDB3Entities = new ECartDB3Entities();

        }
        // GET: Item
        public ActionResult Index()
        {
            ItemViewModel objItemViewModel = new ItemViewModel();
            objItemViewModel.CategorySelectListItem = (from objCat in objECartDB3Entities.Categories
                                                       select new SelectListItem()
                                                       {
                                                           Text = objCat.CategoryName,
                                                           Value = objCat.CategoryId.ToString(),
                                                           Selected = true

                                                       });

            return View(objItemViewModel);
        }

        [HttpPost]
        public JsonResult Index(ItemViewModel objItemViewModel)
        {

            return Json("HHHH", JsonRequestBehavior.AllowGet);
        }

    }
}