using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using VisioConference.DTO;
using VisioConference.Filters;
using VisioConference.Models;
using VisioConference.Repository.DAO;
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
                    ModelState["Password"].Errors.Clear();
                    ModelState["Email"].Errors.Clear();
                    ModelState.AddModelError("Password", "Combinaison nom d'utilisateur et mot de passe incorrecte.");
                    //ViewBag.Erreur = "Echec connexion.....";
                    return View();
                }
            }
            else
            {
                ModelState["Password"].Errors.Clear();
                ModelState["Email"].Errors.Clear();
                ModelState.AddModelError("Login", "Veuillez saisir tous les champs.");
                return View(dto);
            }
        }

        public ActionResult Create([Bind(Include = "Pseudo,Password,Email")] UserDTO userDTO, HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                int currentId = service.findAll().Max(u => u.Id) + 1; // max récupère l'id MAX en BD
                if(Photo != null)
                {
                    userDTO.Photo = userDTO.Pseudo + '_' + currentId + Path.GetExtension(Photo.FileName);
                    //userDTO.Photo = userDTO.Pseudo + 0+ userDTO.Id+ Path.GetExtension(Photo.FileName);
                    Photo.SaveAs(Server.MapPath("~/Content/avatar_user/") + userDTO.Photo);
                }  
                service.Add(userDTO);
                //return RedirectToRoute("Discussion", "Login" );
                Session["userNormal"] = userDTO;
                userDTO.Id = currentId;
                return RedirectToAction("Discussion");
            }
            ModelState["Password"].Errors.Clear();
            ModelState["Email"].Errors.Clear();
            ModelState["Pseudo"].Errors.Clear();
            ModelState.AddModelError("Création", "Veuillez saisir tous les champs");
            return View("Index",userDTO);
        }



        public ActionResult Logout()
        {
            Session.Clear();
            TempData.Remove("Nom_ami");
            TempData.Remove("Id_ami");
            return RedirectToAction("Index", "Login");
        }

        [LoginFilter] //Empeche l'accès si on n'est pas connecté avec une session "userNormal"
        public ActionResult Discussion()
        {
            UserDTO userDTO = (UserDTO)Session["userNormal"];
            List<JointureDTO> friendList;

            //SEARCH FRIEND
            if (TempData["search-friend"] != null)
            {
                ViewBag.ListFriend = Cvservice.findFriends(userDTO, (string)TempData["search-friend"]);
            }

            friendList = Cvservice.findFriends(userDTO);

            //SEARCH MEMBER
            if (TempData["search"] != null)
            {
                TempData["resultatFriendAndOthers"] = Cvservice.findFriendsAndOthers(userDTO, (string)TempData["search"]);
            }
            
            if (TempData["Id_ami"] != null)
            {
                int ami_id = (int)TempData["Id_ami"];
                UserDTO amiDTO = service.findById(ami_id);
                if (ami_id != 0)
                {
                    ConversationDTO CvDto = Cvservice.findByUsers(userDTO, service.findById(ami_id));
                    if (CvDto != null)
                    {
                        if (CvDto.message != null)
                        {
                            List<string> messages = Strings.afficherConv(CvDto.message, userDTO, amiDTO).ToList();
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
            TempData.Remove("search");
            return RedirectToAction("Discussion");

        }
        [HttpPost]
        public ActionResult ClickAmi(System.Web.Mvc.FormCollection form)
        {

            //Afficher nouvelle conversation
            int ami_id = Convert.ToInt32(form.Get("user_id"));
            string nom_ami = service.findById(ami_id).Pseudo;
            TempData["Nom_ami"] = nom_ami;
            TempData["Id_ami"] = ami_id;
            TempData.Keep();
            TempData.Remove("search");
            return RedirectToAction("Discussion");
        }

        [HttpPost]
        public ActionResult SupprimerAmi(System.Web.Mvc.FormCollection form)
        {
            int ami_id = Convert.ToInt32(form.Get("ami_id"));
            UserDTO amidto = service.findById(ami_id);
            UserDTO user = (UserDTO)Session["userNormal"];
            int ConvId = Cvservice.findByUsers(user, amidto).convID;
            Cvservice.removeConversation(ConvId);
            TempData.Remove("search");
            return RedirectToAction("Discussion");
        }


        [HttpPost]
        public ActionResult AcceptInvite(System.Web.Mvc.FormCollection form)
        {
            //Afficher nouvelle conversation
            int ami_id = Convert.ToInt32(form.Get("acceptInviteUser_id"));
            ConversationDTO cv = Cvservice.findByUsers(service.findById(ami_id), (UserDTO)Session["userNormal"]);
            cv.invitation = true;
            Cvservice.Update(cv);
            return RedirectToAction("Discussion");
        }
        [HttpPost]
        public ActionResult RejectInvite(System.Web.Mvc.FormCollection form)
        {
            //Afficher nouvelle conversation
            int ami_id = Convert.ToInt32(form.Get("rejectInviteUser_id"));
            ConversationDTO cv = Cvservice.findByUsers(service.findById(ami_id), (UserDTO)Session["userNormal"]);
            Cvservice.removeConversation(cv.convID);
            TempData["inviteRefusee"] = true;
            TempData.Keep();
            return RedirectToAction("Discussion");
        }

        [HttpPost]
        public ActionResult EnvoyerInvite(System.Web.Mvc.FormCollection form)
        {
            int ami_id = Convert.ToInt32(form.Get("EnvoyerInvite_id"));
            UserDTO user = (UserDTO)Session["userNormal"];
            Cvservice.AddConversation(user.Id, ami_id);
            TempData["inviteEnvoyee"] = true;
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
        public ActionResult TerminerModif()
        {
            return View();

        }
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Password(string EmailID)
        {
            string resetCode = Guid.NewGuid().ToString();
            var verifyUrl = "/Login/ResetPassword/" + resetCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            using (MyContext context = new MyContext())
            {
                var getUser = (from s in context.users where s.Email == EmailID select s).FirstOrDefault();
                if (getUser != null)
                {
                    getUser.ResetPassewordCode = resetCode;

                    //This line I have added here to avoid confirm password not match issue , as we had added a confirm password property 

                    context.Configuration.ValidateOnSaveEnabled = false;
                    context.SaveChanges();

                    var subject = "Password Reset Request";
                    var body = "Bonjour " + getUser.Pseudo + ", <br/> Vous avez demander de modifier votre mot de passe sur YouCom. Veuillez cliquer sur le lien ci dessous pour le réinitialiser. " +

                         " <br/><br/><a href='" + link + "'>" + link + "</a> <br/><br/>" +
                         "Si vous n'êtes pas à l'origine de cette demande, veuillez ignorer cet eMail.<br/><br/> Merci";

                    SendEmail(getUser.Email, body, subject);

                    //ViewBag.Message = "Une e-mail a été envoyé à votre adresse j****@g*****.com.Suivez les instructions fournies pour réinitialiser votre mot de passe.";
                    return View("EmailEnvoyer");
                }
                else
                {
                    ViewBag.Message = "L'utilisateur n'existe pas.";
                    return View("ForgotPassword");
                }
            }
        }

        private void SendEmail(string emailAddress, string body, string subject)
        {
            using (MailMessage mm = new MailMessage("martin.taquet@gmail.com", emailAddress))
            {
                mm.Subject = subject;
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("supprimerdelete01@gmail.com", "AZQSwx123");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);

            }
        }
        public ActionResult ResetPassword(string id)
        {
            //Verify the reset password link
            //Find account associated with this link
            //redirect to reset password page
            if (string.IsNullOrWhiteSpace(id))
            {
                return HttpNotFound();
            }

            using (MyContext context = new MyContext())
            {
                var user = context.users.Where(a => a.ResetPassewordCode == id).FirstOrDefault();
                if (user != null)
                {
                    ResetPasswordModel model = new ResetPasswordModel();
                    model.ResetCode = id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                using (MyContext context = new MyContext())
                {
                    var user = context.users.Where(a => a.ResetPassewordCode == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {
                        //you can encrypt password here, we are not doing it
                        user.Password = model.NewPassword;
                        //make resetpasswordcode empty string now
                        user.ResetPassewordCode = "";
                        //to avoid validation issues, disable it
                        context.Configuration.ValidateOnSaveEnabled = false;
                        context.SaveChanges();
                        message = "Nouveau mot de passe mis à jour avec succès.";
                    }
                }
            }
            else
            {
                message = "Echec de la mis à jour du mot de passe.";
            }
            ViewBag.Message = message;
            return View(model);
        }

        [HttpPost]
        public ActionResult Search(System.Web.Mvc.FormCollection form)
        {
            TempData["search"] = form.Get("search");
            TempData.Keep();
            return RedirectToAction("Discussion");
        }
        
        [HttpPost]
        public ActionResult SearchFriend(System.Web.Mvc.FormCollection form)
        {
            TempData["search-friend"] = form.Get("search-friend");
            TempData.Remove("search");
            TempData.Keep();
            return RedirectToAction("Discussion");
        }
        
        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
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