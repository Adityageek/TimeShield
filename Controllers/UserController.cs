using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeShield.Data;

namespace TimeShield.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Request()
        {
            List<SelectListItem> productList = GetProducts();
            return View(productList);
        }

        public JsonResult RequestInsert(List<Product> products)
        {
            User session = System.Web.HttpContext.Current.Session["UserModel"] as User;
            TimeShield_dbEntities db = new TimeShield_dbEntities();
            foreach (Product product in products)
            {
                Request request = new Request
                {
                    UserId = session.UserId,
                    ProductId = product.ProductId,
                    Quantity = product.Quantity,
                    RequestTime = DateTime.Now,
                }; 
                db.Requests.Add(request);
            }
            int insertedRecords = db.SaveChanges();
            return Json(new { result = "Redirect", url = Url.Action("UserDashBoard", "Account") });
        }

        private List<SelectListItem> GetProducts() {
            TimeShield_dbEntities db = new TimeShield_dbEntities();
            List<SelectListItem> productList = (from p in db.Products.AsEnumerable()
                                                select new SelectListItem
                                                {
                                                    Text = p.Product1,
                                                    Value = p.ProductId.ToString()
                                                }).ToList();
            productList.Insert(0, new SelectListItem { Text = "--Select Product--", Value = "" });
            return productList;

        }
    }
}