using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using BookLibary.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace BookLibary.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        // GET: Roles
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var roles = db.Roles.ToList();
            return View(roles);
        }

        // GET: Roles/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IdentityRole role)
        {

            // TODO: Add insert logic here
            try
            {
                if (!ModelState.IsValid)
                    return View(role);

                db.Roles.Add(role);
                db.SaveChanges();
               
            }
            catch(Exception)
            {
                return View();
            }
            return RedirectToAction("Index", "Roles");


        }

        // GET: Roles/Edit/5
        [HttpGet]
        public ActionResult Edit(string id)
        {
            
            return View();
        }

        // POST: Roles/Edit/5
        [HttpPost]
        public ActionResult Edit(IdentityRole role)
        {
            //try
            //{
            //    if (ModelState.IsValid)
            //    {
            //        db.Entry(role).State = EntityState.Modified;
            //        db.SaveChanges();
            //        return RedirectToAction("Index");
            //    }
            //    return View(role);

            //}
           
                return View(role);
            
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (!id.HasValue)
                return HttpNotFound();

            IdentityRole role = db.Roles.Where(x => x.Id == id.Value.ToString()).FirstOrDefault();

            if (role == null)
                return HttpNotFound();

            return View(role);
        }

        // POST: Roles/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(Guid? id)
        {
           
                // TODO: Add delete logic here
                if (!id.HasValue)
                    return HttpNotFound();

                IdentityRole role = db.Roles.Find(id.Value.ToString());

                if (role == null)
                    return HttpNotFound();

                db.Roles.Remove(role);
                db.SaveChanges();

                return RedirectToAction("Index", "Roles");
            
           
        }

        [HttpGet]
        public ActionResult ManageUserToRoles()
        {
            ViewBag.Roles = db.Roles.Select(x => new SelectListItem { Value = x.Name, Text = x.Name }).ToList(); ;

            return View();
        }

        [HttpPost]
        public ActionResult ManageUserToRolesConfirm(string UserName, string RoleName)
        {
            //check username
            //if (String.IsNullOrEmpty(UserName))
            //    return View();

            //var users = db.Users.Where(x => x.UserName == UserName).FirstOrDefault();

            //if (users == null)
            //{
            //    return HttpNotFound();

            //}

            //try
            //{
            //    ApplicationUser user = db.Users.Where(x => x.Email == UserName).FirstOrDefault();

            //    var _manageUser = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            //    _manageUser.AddToRole(user.Id.ToString(), RoleName);
            //}
            //catch (Exception)
            //{
            //    return View();
            //}

            //return RedirectToAction("Index", "Roles");
            if (String.IsNullOrEmpty(UserName))
                return View();

            var user = db.Users.Where(x => x.UserName == UserName).FirstOrDefault();

            if (user == null)
            {
                return HttpNotFound();

            }

            var _userManage = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            _userManage.AddToRole(user.Id, RoleName);
            return RedirectToAction("Index", "Roles");
        }
    }
}
