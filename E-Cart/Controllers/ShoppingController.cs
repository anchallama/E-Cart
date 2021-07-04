using E_Cart.Models;
using E_Cart.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Cart.Controllers
{
    public class ShoppingController : Controller
    {

        private ECartDB3Entities objECartDB3Entities;
        private List<ShoppingCartModel> ListOfShoppingCartModels;
        public ShoppingController()
        {
            objECartDB3Entities = new ECartDB3Entities();
            ListOfShoppingCartModels = new List<ShoppingCartModel>();
        }
        // GET: Shopping
        public ActionResult Index()
        {

            IEnumerable<ShoppingViewModel> ListOfShoppingViewModels = (from objItem in objECartDB3Entities.Items
                                                                       join
                                                                       objCate in objECartDB3Entities.Categories
                                                                       on objItem.CategoryId equals objCate.CategoryId
                                                                       select new ShoppingViewModel()
                                                                       {
                                                                           ImagePath = objItem.ImagePath,
                                                                           ItemName = objItem.ItemName,
                                                                           Description = objItem.Description,
                                                                           ItemPrice = objItem.ItemPrice,
                                                                           ItemId = objItem.ItemId,
                                                                           Category = objCate.CategoryName,
                                                                           //ItemCode=objCate.ItemCodea
                                                                           ItemCode=objItem.ItemCode
                                                                           
                                                                       }
                                                                    ).ToList();
            return View(ListOfShoppingViewModels);
        }


        [HttpPost]
        public JsonResult Index(string ItemId)
        {



            ShoppingCartModel objshoppingCartModel = new ShoppingCartModel();
            Item objItem = objECartDB3Entities.Items.Single(model => model.ItemId.ToString() == ItemId);

            if (Session["CartCounter"] != null)
            {
                ListOfShoppingCartModels = Session["CartItem"] as List<ShoppingCartModel>;
            }
            if (ListOfShoppingCartModels.Any(model => model.ItemId == ItemId))
            {

                objshoppingCartModel = ListOfShoppingCartModels.Single(model => model.ItemId == ItemId);
                objshoppingCartModel.Quantity = objshoppingCartModel.Quantity + 1;
                objshoppingCartModel.Total = objshoppingCartModel.Quantity * objshoppingCartModel.Quantity;




            }
            else
            {
                objshoppingCartModel.ItemId = ItemId;
                objshoppingCartModel.ImagePath =objItem.ImagePath;
                objshoppingCartModel.ItemName = objItem.ItemName;
                objshoppingCartModel.Quantity = 1;
                objshoppingCartModel.Total = objItem.ItemPrice;
                objshoppingCartModel.UnitPrice = objItem.ItemPrice;
                ListOfShoppingCartModels.Add(objshoppingCartModel);





            }
            Session["CartCounter"] = ListOfShoppingCartModels.Count;
            Session["CartItem"] = ListOfShoppingCartModels;

            return Json(new {Success=true,Counter=ListOfShoppingCartModels.Count },JsonRequestBehavior.AllowGet);
        }



    }
}