using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeShield.Data;
using TimeShield.Models;

namespace TimeShield.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult RequestProduct()
        {
            List<SelectListItem> productList = GetProducts();
            return View(productList);
        }


        public JsonResult RequestInsert(List<Product> products)
        {
            User session = System.Web.HttpContext.Current.Session["UserModel"] as User;
            TimeShield_dbEntities db = new TimeShield_dbEntities();
            Guid guid = Guid.NewGuid();
            foreach (Product product in products)
            {
                Request request = new Request
                {
                    UserId = session.UserId,
                    ProductId = product.ProductId,
                    Quantity = product.Quantity,
                    RequestTime = DateTime.Now,
                    RequestGUID = guid.ToString()
                };
                db.Requests.Add(request);
            }
            int insertedRecords = db.SaveChanges();
            return Json(new { result = "Redirect", url = Url.Action("UserDashBoard", "Account") });
        }

        public ActionResult ApproveRequest()
        {
            ViewBag.RequestId = Request.QueryString["RequestId"];

            return View();
        }

        public ActionResult NewRequest(RequestModel model)
        {
            return View();
        }

        public ActionResult TrackRequest()
        {
            return View();
        }

        public JsonResult GetRequest(string RequestId)
        {
            TimeShield_dbEntities db = new TimeShield_dbEntities();
            db.Configuration.ProxyCreationEnabled = false;
            IEnumerable<RequestModel> request = (from r in db.Requests
                                                 join u in db.Users on r.UserId equals u.UserId
                                                 join p in db.Products on r.ProductId equals p.ProductId
                                                 select new RequestModel
                                                 { UserName = u.UserName, Product1 = p.Product1, Quantity = p.Quantity, RequestGUID = r.RequestGUID, RequestTime = r.RequestTime })
                                                 .Where(r => r.RequestGUID == RequestId).ToList();
            return Json(request, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNewRequest()
        {
            TimeShield_dbEntities db = new TimeShield_dbEntities();
            db.Configuration.ProxyCreationEnabled = false;
            var ad = (from r in db.Requests
                      join u in db.Users on r.UserId equals u.UserId
                      select new RequestModel { UserId = r.UserId, Approveflag = r.Approveflag, RequestGUID = r.RequestGUID, UserName = u.UserName })
                       .Where(a => a.Approveflag == 0).Select(s => new { s.UserId, s.RequestGUID, s.UserName })
                       .GroupBy(a => new { a.RequestGUID, a.UserId, a.UserName }).ToList();
            Dictionary<string, string> list = new Dictionary<string, string>();
            foreach (var a in ad) {
                list.Add(a.Key.RequestGUID, a.Key.UserName);
            }

            List<KeyValuePair<string, string>> re = list.ToList();

            return Json(re, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ApproveRequestInsert(string RequestGUID) {
            TimeShield_dbEntities db = new TimeShield_dbEntities();
            var approve = db.Requests.Where(r => r.RequestGUID == RequestGUID).ToList();
            foreach (var ap in approve) {
                ap.Approveflag = 1;
            }

            db.SaveChanges();
            var approved = db.Requests.Where(r => r.Approveflag == 1 && r.RequestGUID == RequestGUID).Select(a => new { a.RequestGUID, a.UserId }).FirstOrDefault();

            TrackRequest trackRequest = new TrackRequest
            {
                RequestGUID = approved.RequestGUID,
                ApproveTime = DateTime.Now,
                UserId = approved.UserId
            };
            db.TrackRequests.Add(trackRequest);

            db.SaveChanges();

            return Json(new { result = "Redirect", url = Url.Action("NewRequest", "User") }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTrackRequest() {
           
            User session = System.Web.HttpContext.Current.Session["UserModel"] as User;
            TimeShield_dbEntities db = new TimeShield_dbEntities();
            db.Configuration.ProxyCreationEnabled = false;
            IEnumerable<TrackRequest> trackRequest = db.TrackRequests.Where(a => a.UserId == session.UserId).ToList();

            return Json(trackRequest, JsonRequestBehavior.AllowGet);
        }


        #region private methods
        private List<SelectListItem> GetProducts()
        {
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

        #endregion
    }
}