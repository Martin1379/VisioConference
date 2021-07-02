using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisioConference.DTO;
using VisioConference.Service;

namespace VisioConference.Controllers
{
    public class LoginController : Controller
    {
        private UsersService service = new UsersService();


        // GET: Login
        public ActionResult Index()
        {
            UserDTO dto = new UserDTO();
            return View(dto);
        }

        [HttpPost]
        public ActionResult Index(UserDTO dto)
        {
            if (ModelState.IsValidField("Email") && ModelState.IsValidField("Password"))
            {
                UserDTO user = service.findByEmailAndPassword(dto);
                if (user != null && user.Id != 0)
                {
                    if (user.IsAdmin)
                    {
                        Session["userAdmin"] = user;
                        return RedirectToAction("Accueil");
                    }
                    else
                    {
                        Session["userNormal"] = user;
                        return RedirectToAction("Accueil"); ;
                    }

                }
                else
                {
                    ViewBag.Erreur = "Echec connexion.....";
                    return View(dto);
                }
            }
            else
            {
                return View(dto);
            }
        }


        public ActionResult Create([Bind(Include = "Pseudo,Password,Email")] UserDTO userDTO, HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                userDTO.Photo = userDTO.Pseudo + Path.GetExtension(Photo.FileName);
                Photo.SaveAs(Server.MapPath("~/Content/images/") + userDTO.Photo);

                service.Add(userDTO);
                return RedirectToAction("Index");
            }

            return View(userDTO);
        }



        public ActionResult Logout()
        {
            //Session["userAdmin"] = null;
            //Session.Abandon();
            Session.Clear();


            return RedirectToAction("Index");
        }

        public ActionResult Accueil()
        { 
            return View();
        }
    }
}