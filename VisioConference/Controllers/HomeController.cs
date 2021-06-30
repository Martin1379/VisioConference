using System.Collections.Generic;
using System.Web.Mvc;
using VisioConference.DTO;
using VisioConference.Models;
using VisioConference.Repository.DAO;
using VisioConference.Service;

namespace VisioConference.Controllers
{
    public class HomeController : Controller
    {
        private UserService service = new UserService();
        public ActionResult Index()
        {
            return View(new UserDTO());
        }

        [HttpPost]
        public ActionResult Index(UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                service.Add(userDTO);
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View(userDTO);
            };
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

            User us2 = new User(4,"Jean");
            ConversationDAO cdao = new ConversationDAO();
            List<User> lst2 = new List<User>();
            lst2 = cdao.findFriends(us2);
            ViewBag.Data2 = lst2;



            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}