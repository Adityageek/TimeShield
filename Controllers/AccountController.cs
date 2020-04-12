using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeShield.Data;
using TimeShield.Models;

namespace TimeShield.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                using (TimeShield_dbEntities db = new TimeShield_dbEntities())
                {
                    var objUser = db.Users.Where(a => a.UserName.Equals(userModel.UserName) && a.Password.Equals(userModel.Password)).FirstOrDefault();
                    if (objUser != null)
                    {
                        Session["UserModel"] = objUser;
                        return RedirectToAction("Request", "User");
                    }
                }
            }
            return View(userModel);
        }

        public ActionResult UserDashBoard()
        {
            return View();
        }
    }
    
}