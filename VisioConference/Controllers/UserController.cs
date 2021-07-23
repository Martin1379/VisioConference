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
    [UserFilter]
    public class UserController : Controller
    {
        private UsersService service = new UsersService();

            // GET: User
            public ActionResult Index(string search, int? i, string sortBy)
        {
            List<UserDTO> lst = new List<UserDTO>();
            
            
            if (search != null)
                //lst = (List<UserDTO>)service.findAll().Where(u => u.Pseudo.Contains(search)).ToList().Union(service.findAll().Where(u => u.Email.Contains(search)).ToList());
                lst = service.findAll(search).ToList();
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

            return View(lst.ToPagedList(i ?? 1, 10));
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

        
        public ActionResult Create()
        {
            return View(new UserDTO());
        }

        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Pseudo,Email,Password,Etat,IsAdmin")] UserDTO userDTO, HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                if(Photo != null)
                {
                    int currentId = service.findAll().Max(u => u.Id) + 1; // max récupère l'id MAX en BD
                    userDTO.Photo = userDTO.Pseudo + '_' + currentId + Path.GetExtension(Photo.FileName);
                    Photo.SaveAs(Server.MapPath("~/Content/avatar_user/") + userDTO.Photo);

                }

                service.Add(userDTO);
                return RedirectToAction("Index");
            } 

            return View(userDTO);
        }
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

        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Pseudo,Email,Password,Etat,IsAdmin")] UserDTO userDTO, HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    //ToDO : Supprimer la photo d'origine (différentes extensions = pas d'écrasement)
                    userDTO.Photo = userDTO.Pseudo + '_' + userDTO.Id + Path.GetExtension(Photo.FileName);
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

        //TODO : rajouter la supression des lignes dans conversation pour tous les endroits où friendId/UserId correspond à l'id à supprimer
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            service.DeleteUserDTO(id);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DisplayFriends(int? id, int? i, string sortBy)
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
                ConversationService Cvservice = new ConversationService();
                List<JointureDTO> friendList= Cvservice.findFriends(userDTO);
                //friendList = Cvservice.findFriends(userDTO);
                //Tri
                switch (sortBy)
                {
                    case "nameAsc":
                        friendList = friendList.OrderBy(x => x.Pseudo).ToList();
                        break;
                    case "nameDesc":
                        friendList = friendList.OrderByDescending(x => x.Pseudo).ToList();
                        break;
                    case null:
                        break;
                    default:
                        break;
                }

                Session["edituser"] = userDTO.Id;
                return View(friendList.ToPagedList(i ?? 1, 10));
                
            }
        }

        public ActionResult Remove(int? friendId)
        {

            if (/*id == null || */friendId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //UserDTO userDTO = service.findById(id);
            UserDTO friendDTO = service.findById(friendId);
            if (/*userDTO == null ||*/ friendDTO==null)
            {
                return HttpNotFound();
            }
            else
            {
                //ConversationService Cvservice = new ConversationService();
                //ConversationDTO Cv = Cvservice.findByUsers(userDTO, friendDTO);
                return View(friendDTO);
            }

            
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveConfirmed(int friendId)
        {
            int idUser = (int)Session["edituser"];
            UserDTO userDTO = service.findById(idUser);
            UserDTO friendDTO = service.findById(friendId);

            ConversationService Cvservice = new ConversationService();
            ConversationDTO Cv = Cvservice.findByUsers(userDTO, friendDTO);

            try
            {
                Cvservice.removeConversation(Cv.convID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

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
