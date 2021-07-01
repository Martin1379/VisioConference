using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VisioConference.DTO;
using VisioConference.Repository.DAO;
using VisioConference.Service;

namespace VisioConference.Controllers
{
    public class UserController : Controller
    {
        private UsersService service = new UsersService();

        // GET: User
        public ActionResult Index()
        {
            return View(service.findAll());
        }

        // GET: User/Details/5
    //    public ActionResult Details(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        UserDTO userDTO = db.UserDTOes.Find(id);
    //        if (userDTO == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(userDTO);
    //    }

    //    // GET: User/Create
    //    public ActionResult Create()
    //    {
    //        return View();
    //    }

    //    // POST: User/Create
    //    // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
    //    // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Create([Bind(Include = "Id,Pseudo,Email,Password,Photo,Connected,IsAdmin")] UserDTO userDTO)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            db.UserDTOes.Add(userDTO);
    //            db.SaveChanges();
    //            return RedirectToAction("Index");
    //        }

    //        return View(userDTO);
    //    }

    //    // GET: User/Edit/5
    //    public ActionResult Edit(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        UserDTO userDTO = db.UserDTOes.Find(id);
    //        if (userDTO == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(userDTO);
    //    }

    //    // POST: User/Edit/5
    //    // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
    //    // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Edit([Bind(Include = "Id,Pseudo,Email,Password,Photo,Connected,IsAdmin")] UserDTO userDTO)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            db.Entry(userDTO).State = EntityState.Modified;
    //            db.SaveChanges();
    //            return RedirectToAction("Index");
    //        }
    //        return View(userDTO);
    //    }

    //    // GET: User/Delete/5
    //    public ActionResult Delete(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        UserDTO userDTO = db.UserDTOes.Find(id);
    //        if (userDTO == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(userDTO);
    //    }

    //    // POST: User/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult DeleteConfirmed(int id)
    //    {
    //        UserDTO userDTO = db.UserDTOes.Find(id);
    //        db.UserDTOes.Remove(userDTO);
    //        db.SaveChanges();
    //        return RedirectToAction("Index");
    //    }

    //    protected override void Dispose(bool disposing)
    //    {
    //        if (disposing)
    //        {
    //            db.Dispose();
    //        }
    //        base.Dispose(disposing);
    //    }
    }
}
