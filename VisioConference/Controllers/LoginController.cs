using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisioConference.DTO;
using VisioConference.Filters;
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
                        return RedirectToAction("Index", "User");
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
                userDTO.Photo = userDTO.Pseudo + 0+ userDTO.Id+ Path.GetExtension(Photo.FileName);
                Photo.SaveAs(Server.MapPath("~/Content/avatar_user/") + userDTO.Photo);

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
        [LoginFilter] //Empeche l'accès si on n'est pas connecté avec une session "userNormal"
        public ActionResult Accueil()
        { 
            return View();
        }

        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}