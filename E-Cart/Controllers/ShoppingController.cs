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
        public ShoppingController()
        {
            objECartDB3Entities = new ECartDB3Entities();
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
                                                                           Category = objCate.CategoryName
                                                                       }
                                                                    ).ToList();
            return View(ListOfShoppingViewModels);
        }
    }
}