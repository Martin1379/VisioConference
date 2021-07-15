using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using VisioConference.DTO;
using VisioConference.Filters;
using VisioConference.Service;
using VisioConference.Tools;

namespace VisioConference.Controllers
{
    public class LoginController : Controller
    {
        private UsersService service = new UsersService();
        private ConversationService Cvservice = new ConversationService();


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
                        return RedirectToAction("Discussion"); ;
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
                int currentId = service.findAll().Max(u => u.Id) + 1; // max récupère l'id MAX en BD
                userDTO.Photo = userDTO.Pseudo + '_' + currentId + Path.GetExtension(Photo.FileName);
                //userDTO.Photo = userDTO.Pseudo + 0+ userDTO.Id+ Path.GetExtension(Photo.FileName);
                Photo.SaveAs(Server.MapPath("~/Content/avatar_user/") + userDTO.Photo);

                service.Add(userDTO);
                //return RedirectToRoute("Discussion", "Login" );
                Session["userNormal"] = userDTO;
                userDTO.Id = currentId;
                return RedirectToAction("Discussion");
            }
            return View(userDTO);
        }



        public ActionResult Logout()
        {
            DialogResult dialogResult = MessageBox.Show("Voulez-vous réellement déconnecter ?", "Déconnextion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }
            else 
            {
                return RedirectToAction("Discussion", "Login");
            }

        }


        [LoginFilter] //Empeche l'accès si on n'est pas connecté avec une session "userNormal"
        public ActionResult Discussion()
        {
            UserDTO userDTO = (UserDTO)Session["userNormal"];
            List<UserDTO> friendList = Cvservice.findFriends(userDTO);

            if (TempData["Id_ami"] != null)
            {
                int ami_id = (int)TempData["Id_ami"];
                if (ami_id != 0)
                {
                    ConversationDTO CvDto = Cvservice.findByUsers(userDTO, service.findById(ami_id));
                    if (CvDto != null)
                    {
                        if(CvDto.message != null)
                        {
                            List<string> messages = Strings.afficherConv(CvDto.message).ToList();
                            ViewBag.Messages = messages;
                        }

                    }
                    TempData.Keep();
                }
            }

            return View(friendList);
        }

        public ActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EnvoyerMessage(System.Web.Mvc.FormCollection form)
        {
            if (TempData["Id_ami"] != null)
            {
                UserDTO utilisateur = (UserDTO)Session["userNormal"];
                UserDTO ami = service.findById((int)TempData["Id_ami"]);
                ConversationDTO CvDto = Cvservice.findByUsers(utilisateur, ami);
                string contenu = CvDto.message + "<#" + utilisateur.Id + '>' + form.Get("message_envoye");
                Cvservice.modifyMessage(CvDto, contenu);

            }
            


            //TempData["message"]= contenu;
            //TempData.Keep();
            return RedirectToAction("Discussion");

        }

        [HttpPost]
        public ActionResult ClickAmi(System.Web.Mvc.FormCollection form)
        {
            //Afficher nouvelle conversation
            int ami_id = Convert.ToInt32(form.Get("user_id"));
            TempData["Id_ami"] = ami_id;
            TempData.Keep();
            return RedirectToAction("Discussion");

        }
        [HttpPost]
        public ActionResult SaveProfil(System.Web.Mvc.FormCollection form, HttpPostedFileBase Photo)
        {
            UserDTO userDTO = (UserDTO)Session["userNormal"];
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
            UserDTO usermodif = new UserDTO()
            {
                Id = userDTO.Id,
                Email = userDTO.Email,
                Pseudo = form.Get("Pseudo"),
                Password = form.Get("Password"),
                Photo = userDTO.Photo
            };
            service.Update(usermodif);
            Session["userNormal"] = usermodif;
            return RedirectToAction("Discussion");

        }
    }
}
/*
 * ViewResult - View()
 * RedirectToRouteResult - RedirectToAction(), RedirectToRoute()...
 * ContentResult - Content()
 * FileContentResult - File()
 * JavaScript - Javascript()
 * JSONResult - JSON()
 * EmptyResult - null
 * HttpNotFoundResult - HttpNotFound()
 * 
 * Stocker des données de type clés-valeur
 * ViewBag - ViewData : dès que l'action est executée, le dictionnaire est remis à 0
 * TempData : conserve des données entre différentes actions grâce à la méthode keep
 * 
 * Session : valable pendant toute la durée de la session (20-30 minutes par défaut) - Géré côté serveur (pas côté client)
 * 
 */