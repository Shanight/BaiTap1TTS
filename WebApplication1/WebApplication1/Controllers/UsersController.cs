using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebApplication1.Models;
using static System.Collections.Specialized.BitVector32;

namespace WebApplication1.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            if (TempData.ContainsKey("ErrorMessage"))
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }
            Users model = new Users();
            DataTable dt = model.GetAll();
            return View("Index", dt);
        }

        // GET: Users/Details/5
        public ActionResult Detail(string UserID)
        {
            Users model = new Users();
            DataTable dt = model.GetUserByID(UserID);
            return View("Detail", dt);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Users/Create
        [HttpPost]
        public ActionResult Create(string UserID, string UserName, string Password, string Email, string Tel, string Disabled, string action)
        {
            if (action == "Create")
            {
                Users model = new Users();

                // Kiểm tra trùng lặp IDUser
                bool isExistingUser = model.CheckExistingUser(UserID);
                if (isExistingUser)
                {
                    // UserID đã tồn tại trong cơ sở dữ liệu, xử lý lỗi tại đây
                    TempData["ErrorMessage"] = "Lỗi, đã trùng ID.";
                    return RedirectToAction("Index");
                }

                string UserIDtodtb = UserID.ToUpper();
                string UserNametodtb = UserName;
                string Passwordtodtb = Password;
                string Emailtodtb = Email;
                string Teltodtb = Tel;
                string Disabledtodtb = Disabled != null ? Disabled : "0";
                string status = model.InsertUsers(UserIDtodtb, UserNametodtb, Passwordtodtb, Emailtodtb, Teltodtb, Disabledtodtb);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string UserID)
        {
            Users model = new Users();
            DataTable dt = model.GetUserByID(UserID);
            return View("Edit", dt);
        }

        /// <summary>
        /// Action method, called when users update the record or cancel the update.
        /// </summary>
        
        /// <returns>Home view</returns>
        public ActionResult Update(FormCollection dtb, string action)
        {
            if (action == "Submit")
            {
                Users model = new Users();
                string UserID = dtb["UserID"].ToUpper();
                string UserName = dtb["UserName"];
                string Password = dtb["Password"];
                string Email = dtb["Email"];
                string Tel = dtb["Tel"];
                string Disabled = dtb["Disabled"] != null ? dtb["Disabled"] : "0";
                
                int status = model.UpdateUser(UserID, UserName, Password, Email, Tel, Disabled);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string UserID)
        {
            Users model = new Users();
            model.DeleteUser(UserID);
            return RedirectToAction("Index");
        }
    }
}
