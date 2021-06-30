using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisioConference.Repository.DAO;
using VisioConference.Models.Objets;


namespace VisioConference.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
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