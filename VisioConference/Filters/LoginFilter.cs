using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VisioConference.Filters
{
    public class LoginFilter : ActionFilterAttribute
    {
        //Vérifier si le User est connecté ou pas, admin ou normal
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["userAdmin"]== null && filterContext.HttpContext.Session["userNormal"] == null)
            {
                //redirection => page login
                filterContext.HttpContext.Response.Redirect("~/Login/Index");
            }
            else //Si une session, admin ou normale est active
            {
                if (filterContext.HttpContext.Session["userAdmin"] != null)
                {
                    //Pas accès aux conversation
                    filterContext.HttpContext.Response.Redirect("~/Login/Index");
                }
                else if (filterContext.HttpContext.Session["userNormal"] != null)
                {
                    //Pas accès aux parties admin
                    filterContext.HttpContext.Response.Redirect("~/Login/Accueil");
                }
            }
        }
    }
}