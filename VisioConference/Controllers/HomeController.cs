using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using VisioConference.DTO;
using VisioConference.Models;
using VisioConference.Repository.DAO;
using VisioConference.Service;

namespace VisioConference.Controllers
{
    public class HomeController : Controller
    {
        private UsersService service = new UsersService();
        public ActionResult Index()
        {
            return View(new UserDTO());
        }

        [HttpPost]
        public ActionResult Index(UserDTO userDTO, HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                userDTO.Photo = userDTO.Pseudo + Path.GetExtension(Photo.FileName);
                Photo.SaveAs(Server.MapPath("~/Content/img/") + userDTO.Photo);
                service.Add(userDTO);
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View(userDTO);
            };
        }

        public ActionResult Main()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            
            UserDAO udao = new UserDAO();

            //User us = new User();
            //us = udao.findByLogin("Remy");
            //ViewBag.Variable = us.Id;

            //List<User> lst = new List<User>();
            //lst = udao.findAll();
            //ViewBag.Data = lst;

            UserDTO us2 = new UserDTO(4,"Jean");
            ConversationDAO cdao = new ConversationDAO();
            List<UserDTO> lst2 = new List<UserDTO>();
            lst2 = cdao.findFriends(us2);
            ViewBag.Data2 = lst2;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Logout()
        {
            //à voir avec Mourad s'il est nécessaire d'avoir deux type de sessions différentes
            //Session["userAdmin"] = null;
            //Session["userNormal"] = null;
            Session.Clear();
            return View();
        }
    }
}