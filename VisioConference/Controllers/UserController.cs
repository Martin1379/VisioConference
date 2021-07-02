using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VisioConference.DTO;
using VisioConference.Filters;
using VisioConference.Repository.DAO;
using VisioConference.Service;

namespace VisioConference.Controllers
{
    //[LoginFilter]
    public class UserController : Controller
    {
        private UsersService service = new UsersService();

        // GET: User
        public ActionResult Index(string search, int? i, string sortBy)
        {
            List<UserDTO> lst = new List<UserDTO>();

            //Filtre
            if (search != null)
                lst = service.findAll().Where(u => u.Pseudo.Contains(search)).ToList();

            else
                lst = service.findAll();

            //Tri
            switch (sortBy)
            {
                case "nameAsc":
                    lst = lst.OrderBy(x => x.Pseudo).ToList();
                    break;
                case "nameDesc":
                    lst = lst.OrderByDescending(x => x.Pseudo).ToList();
                    break;
                case null:
                    break;
                default:
                    break;
            }

            return View(lst.ToPagedList(i ?? 1, 2));
        }

        /*GET: User/Details/5*/
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDTO userDTO = service.findById(id);
            if (userDTO == null)
            {
                return HttpNotFound();
            }
            return View(userDTO);
        }

            // GET: User/Create
        public ActionResult Create()
        {
            return View(new UserDTO());
        }

        // POST: User/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Pseudo,Email,Password,Connected,IsAdmin")] UserDTO userDTO, HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                userDTO.Photo = userDTO.Pseudo + userDTO.Id +Path.GetExtension(Photo.FileName);
                Photo.SaveAs(Server.MapPath("~/Content/avatar_user/") + userDTO.Photo);

                service.Add(userDTO);
                return RedirectToAction("Index");
            }

            return View(userDTO);
        }

        //    // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDTO userDTO = service.findById(id);
            if (userDTO == null)
            {
                return HttpNotFound();
            }
            else
            {
                Session["image"] = userDTO.Photo;
                return View(userDTO);

            }
        }

        // POST: User/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Pseudo,Email,Password,Connected,IsAdmin")] UserDTO userDTO, HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    //ToDO : Supprimer la photo d'origine (différentes extensions = pas d'écrasement)
                    userDTO.Photo = userDTO.Pseudo + userDTO.Id + Path.GetExtension(Photo.FileName);
                    Photo.SaveAs(Server.MapPath("~/Content/avatar_user/") + userDTO.Photo);

                    //service.Add(userDTO);
                    //return RedirectToAction("Index");
                }
                else
                {
                    userDTO.Photo = (string)Session["image"];
                    Session.Remove("image");
                }
                service.Update(userDTO);
                return RedirectToAction("Index");
            }
            return View(userDTO);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDTO userDTO = service.findById(id);
            if (userDTO == null)
            {
                return HttpNotFound();
            } else
            {
                Session["image"] = userDTO.Photo;
                return View(userDTO);
            }
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            service.DeleteUserDTO(id);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
